using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace ChatServer
{
    class Program
    {

        static string ipAddress;            //переменная для ip-адреса сервера
        static int port;                    //переменная для порта сервера
        static Dictionary<string, Action> commands; //команды для управления работой сервера
        static Dictionary<string, Action<string, Letter>> parseCom; //команды, приходящие от клиентов
        static bool ServerEnabled;          //переменная, отображающая, работает ли сервер
        static Task ServerWork;             //задача, в которой выполняется работа сервера
        static bool IsWork;                 //переменная, отображающая нужно ли приложение
        static Socket listener;             //сокет, прослушивающий порт
        static Task Connector;              //задача, в которой будет происходить обработка сообщений клиентов
        static bool ComStopServer;          //переменная, отвечающая за отключение сервера
        static List<Chater> Users;    //список людей онлайн
        
        static void Main(string[] args)
        {
            IsWork = true;                  //приложение включается
            ServerEnabled = false;          //сервер отключен
            ComStopServer = false;          //команды на отключение сервера не было
            commands = new Dictionary<string, Action>();    //создаем словарь команд управления сервером
            commands.Add("help", Help);         //добавляем справку
            commands.Add("start", StartServer); //добавляем команду старта сервера
            commands.Add("stop", StopServer);   //добавляем команду остановки сервера
            commands.Add("exit", Close);        //добавляем команду выхода из приложения
            commands.Add("settings", SetSettings);  //добавляем команду настроек
            ParseComAdd();                  //добавляем список команд, приходящих от клиентов
            Console.Write("Enter command (for help enter \"help\"):");  //вводное слово
            string command;
            while (IsWork)      //пока необходимо, чтобы приложение работало
            {
                command = Console.ReadLine();       //считывваем коману
                command = command.ToLower();        //переводим в нижний регистр команду
                if (commands.ContainsKey(command)) commands[command]();     //выполняем ее
                else Console.WriteLine("Command {0} not found", command);   //если такой команды нет, сообщаем об этом
            }
            
            
        }
        static void Help()          //вывод справки 
        {
            Console.WriteLine("==================================\n");
            Console.WriteLine("\t\tHELP");
            Console.WriteLine("----------------------------------\n");
            Console.WriteLine("Commands:");
            Console.WriteLine("help - View Help;");
            Console.WriteLine("start - Start Chat Server;");
            Console.WriteLine("stop - Stop Chat Server;");
            Console.WriteLine("settings - Server Work Settings");
            Console.WriteLine("exit - Close Application.\n");
            Console.WriteLine("Just that`s all.=)");
            Console.WriteLine("==================================\n\n");
        }
        static void StartServer()   //старт сервера
        {
            if (!ServerEnabled)     //если сервер не активен, то
            {
                SetSettings();          //то запускает настройку сервера
                Users = new List<Chater>();  //создаем экземпляр списка пользователей
                if (!File.Exists(ipAddress + "\\users.txt"))
                {
                    FileStream fs = File.Create(ipAddress + "\\users.txt");
                    fs.Close();
                }
                else
                {
                    StreamReader sr = new StreamReader(ipAddress + "\\users.txt");
                    while (!sr.EndOfStream)
                    {
                        Users.Add(new Chater(sr.ReadLine()));
                    }
                    sr.Close();
                }
                ServerWork = new Task(ServerWorking, TaskCreationOptions.LongRunning); //создаем задачу для запуска сервера в потоке не из пула
                
                ServerWork.Start();     //запускаем сервер
            }
            else            //а если включен, то
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chat Server was started early.");    //пишем красным, что он включен
                Console.ResetColor();
            }
        }
        static void ServerWorking()     //метод, описывающий работу сервера
        {
            ComStopServer = false;      //команды на остановку сервера не было
            Console.WriteLine("Starting Chat Server...");
            listener = new Socket(AddressFamily.InterNetwork,       
                SocketType.Stream,
                ProtocolType.Tcp);      //создаем сокет для прослушивания порта
            listener.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), port));    //привязываем к сокету адрес и порт
            listener.Listen(10);        //и устанавливаем максимальное количество очереди в 10 (можно и больше)
            ServerEnabled = true;       //записываем в переменную, что сервер запущен
            Console.WriteLine("Chat Server succesfully started.");  //это же выводим на экран
            while (ServerEnabled)       //пока сервер работает
            {
                if (Connector == null ||        
                    Connector.Status == TaskStatus.RanToCompletion)
                    //проверяем, свободна ли задача обработки входящих сообщений от клиентов
                {
                        Connector = new Task(ConnectionHandler);    //если да, то создаем новую задачу
                        Connector.Start(); //и запускаем ее
                }
                if (ComStopServer)      //проверяем, не поступало ли запроса на остановку сервера
                {
                    if (Connector.Status == TaskStatus.Running) Connector.Wait(50);
                    //ждем, может сервер успеет обработать оставшиеся запросы
                    listener.Close();   //и закрываем сокет-прослушиватель порта
                    ServerEnabled = false;  //пишем в переменную, что сервер отключен
                    Console.WriteLine("Chat Server Stopped.");  //то же самое выводим на экран
                }

            }
        }
        static void ConnectionHandler()     //обработчик входящих подключений
        {
            Socket recieveMes = null;       //создаем сокет-обработчик
            try             //необходим, чтобы не вылетал эксепшен при отключении сервера
            {
                recieveMes = listener.Accept(); //ставим сокет на подключение к первому в очереди прослушивателя
                string recievedValue = string.Empty;    //чистим строку для входящего сообщения
                while (true)    //бесконечный цикл
                {
                    byte[] recievedBytes = new byte[1024];      //буфер для входящего потока байтов
                    int numBytes = recieveMes.Receive(recievedBytes);   //количество пришедших байтов
                    recievedValue += Encoding.ASCII.GetString(recievedBytes, 0, numBytes);  //переводим поток байтов в строку
                    if (recievedValue.IndexOf("{FINAL}") > -1) break;   //если встречаем конец сообщения, выходим из цикла
                }
                string from =recieveMes.RemoteEndPoint.ToString();
                from = from.Substring(0, from.IndexOf(':'));
                from = "{FROM}" + from;
                recievedValue = recievedValue.Insert(0, from);
                Letter l = Parser(recievedValue);
                //if (l.type == MesType.Message)
                //{
                //    SendMessage(l);
                //}


                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("From: {0}\t To: {1}", recieveMes.RemoteEndPoint.ToString(), "Ola");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Text: {0}", recievedValue);
                Console.ResetColor();
                Console.WriteLine(((IPEndPoint)recieveMes.RemoteEndPoint).Address.ToString());
                string replyValue = "{OK}";
                byte[] replyMessage = Encoding.ASCII.GetBytes(replyValue);  //переводим ответное сообщение в байты
                recieveMes.Send(replyMessage);      //отправляем подтверждение об обработки сообщения
                recieveMes.Shutdown(SocketShutdown.Both);       //отключаем сокет
                recieveMes.Close();     //и уничтожаем его
            }
            catch (SocketException)     //если сервер отключается
            {
                if (recieveMes != null) recieveMes.Close();     //и наш сокет существует, то закрываем его
            }
        }
        static void StopServer()        //остановка сервера
        {
            if (ServerEnabled)  //если сервер работает, то
            {
                ComStopServer = true;       //ставим метку об отключении
                Console.WriteLine("Chat Server stopping...");
            }
            else                //в противном случае
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chat Server disabled.");     //красным пишем, что он не работает
                Console.ResetColor();
            }
        }
        static void Close()         //выход из приложения
        {
            if (ServerEnabled) StopServer();    //если сервер работает, отключаем его
            IsWork = false;     // и ставим метку о выходе из приложения
        }
        static void SetSettings()       //настройки
        {
            /* Первый вариант
            FileStream fs = File.Open("settings.txt", FileMode.OpenOrCreate);//открываем или создаем файл настроек
            if (fs.Length>0)    //если он не пустой
            {
                byte[] temp = new byte[1024];
                fs.Read(temp, 0, (int)fs.Length);
                string tempS = Encoding.ASCII.GetString(temp);
                ipAddress = tempS.Substring(0, tempS.IndexOf('\n'));
                port = int.Parse(tempS.Substring(tempS.IndexOf('\n')));
                Console.WriteLine("Current IP-address: " + ipAddress);
                Console.WriteLine("Current port: " + port);
                Console.Write("Do you want change settings (y/n)? ");
                if (Console.ReadLine() != "y")
                {
                    fs.Close();
                    return;
                }
            }
            else
            {
                Console.WriteLine("No settings for Chat Server.");
            }
            fs.Close();
            fs = File.Create("settings.txt");
            Console.Write("Enter server IP Address:");  //спрашиваем к какому ip подключать сервер
            ipAddress = Console.ReadLine();
            Console.Write("Enter server number of port:");  //и на какой порт
            port = Convert.ToInt32(Console.ReadLine());
            string tempS1 = ipAddress + "\n" + port.ToString();
            byte[] temp1 = Encoding.ASCII.GetBytes(tempS1);
            fs.Write(temp1, 0, temp1.Length);
            fs.Close();
            Directory.CreateDirectory(ipAddress);
            */
            ///*    Второй вариант
            if (!File.Exists("settings.txt"))
            {
                FileStream fs = File.Create("settings.txt");
                fs.Close();
            }
            StreamReader sr = new StreamReader("settings.txt");
            if (!sr.EndOfStream)
            {
                ipAddress = sr.ReadLine();
                port = Convert.ToInt32(sr.ReadLine());
                Console.WriteLine("Current IP-address: " + ipAddress);
                Console.WriteLine("Current port: " + port);
                Console.Write("Do you want change settings (y/n)? ");
                if (Console.ReadLine() != "y")
                {
                    sr.Close();
                    return;
                }
            }
            else
            {
                Console.WriteLine("No settings for Chat Server.");
            }
            sr.Close();
            Console.Write("Enter server IP Address:");  //спрашиваем к какому ip подключать сервер
            ipAddress = Console.ReadLine();
            Console.Write("Enter server number of port:");  //и на какой порт
            port = Convert.ToInt32(Console.ReadLine());
            StreamWriter sw = new StreamWriter("settings.txt"); //открываем файл с настройками
            sw.WriteLine(ipAddress);        //и записываем туда адрес
            sw.WriteLine(port);             // и порт
            sw.Close();         //закрываем файл
            Directory.CreateDirectory(ipAddress);
            //*/
        }
        static Letter Parser(string mes)
        {
            string[] infos = mes.Split(new char[] { '{' }, StringSplitOptions.RemoveEmptyEntries);
            string[] coms = new string[infos.Length];
            for (int i = 0; i < coms.Length; i++)
            {
                coms[i] = infos[i].Substring(0, infos[i].IndexOf('}'));
                infos[i] = infos[i].Substring(infos[i].IndexOf('}') + 1,
                    infos[i].Length - infos[i].IndexOf('}') - 1);
            }
            Letter temp = new Letter();
            for (int i=0; i<coms.Length; i++)
            {
                if (temp.type == MesType.Error) break;
                if (parseCom.ContainsKey(coms[i]))
                    parseCom[coms[i]](infos[i], temp);
                else
                {
                    temp.type = MesType.Error;
                }
            }
            //switch (coms[0])
            //{
            //    case "STATUS": temp.type = MesType.Status; break;
            //    case "MESSAGE": temp.type = MesType.Message; break;
            //    case "ADD": temp.type = MesType.AddFriend; break;
            //    default: temp.type = MesType.Error; break;
            //}
            //temp.id

            return temp;
        }
        static void ParseComAdd()
        {
            parseCom = new Dictionary<string, Action<string,Letter>>();
            parseCom.Add("REGISTRATION", RecieveRegistration);
            parseCom.Add("FROM", WhoSender);
            parseCom.Add("STATUS", RecieveStatus);
            parseCom.Add("MESSAGE", RecieveMessage);
            parseCom.Add("TEXT", RecieveText);
            parseCom.Add("TO", RecieveTo);
            parseCom.Add("FINAL", GoodMes);
        }
        static void WhoSender (string info, Letter obj)
        {
            foreach (Chater user in Users)
            {
                if (info == user.ip)
                {
                    obj.From = user;
                    break;
                }
            }
            if (obj.From==null)
            {
                obj.From = new Chater(info + "\tunknown_user");
                obj.type = MesType.Error;
                obj.Text = "E001";
            }
        }
        static void RecieveRegistration(string info, Letter obj)
        {
            obj.type = MesType.Registration;
            obj.id = info;
        }
        static void RecieveStatus(string info, Letter obj)
        {
            obj.type = MesType.Status;
            obj.id = info;
            //switch (info)
            //{
            //    case "ONLINE": obj.From.status = Chater.Status.Online; break;
            //    case "OFFLINE": obj.From.status = Chater.Status.Offline; break;
            //    default: break; //тут должна быть ошибка
            //}
        }
        static void RecieveMessage(string info, Letter obj)
        {
            obj.type = MesType.Message;
            obj.id = info;
        }
        static void RecieveText(string info, Letter obj)
        {
            obj.Text = info;
        }
        static void RecieveTo(string info, Letter obj)
        {
            foreach (Chater user in Users)
            {
                if (info == user.ip)
                {
                    obj.To = user;
                    break;
                }
            }
            if (obj.To == null)
            {
                obj.To = new Chater(info + "\tunknown_user");
                obj.type = MesType.Error;
                obj.Text = "E002";
            }
        }
        static void GoodMes(string info, Letter obj)
        {
        }
        static async void SendMessage(Letter l)
        {
            await Task.Run(() =>
            {
                Socket s = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                s.Connect(new IPEndPoint(IPAddress.Parse(l.To.ip), port));
                string mes = "{MESSAGE}" + l.id + "{FROM}" + l.From.ip + "{TEXT}" + l.Text + "{FINAL}";
                byte[] sendBytes = Encoding.ASCII.GetBytes(mes);
                s.Send(sendBytes);
                s.Close();
            });
        }
    }
    enum MesType { Registration, Status, Message, Error }; //перечисление форм сообщений
    class Chater
    {
        public enum Status { Online, Offline };
        public string ip;
        public string name;
        public Status status;
        

        public Chater(string chater)
        {
            string[] temp = chater.Split('\t');
            this.ip = temp[0];
            this.name = temp[1];
            status = Status.Offline;
        }
        public bool IsOnline()
        {
            if (status == Status.Offline) return false;
            else return true;
        }
    }
    class Letter               //класс для формирования сообщения
    {
        public MesType type;
        public string id;
        public Chater From;
        public Chater To;
        public string Text;
    }
}
