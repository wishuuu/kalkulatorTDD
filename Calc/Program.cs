using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CalcClassProject;

namespace Calc
{
    class Program
    {
        private static ICalcClass _calcClass = new CalcClass();

        static void Main(string[] args)
        {
            while (true)
            {
                string message = "";
                var factory = new ConnectionFactory() {HostName = "localhost"};
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        message = Encoding.UTF8.GetString(body);
                    };
                    channel.BasicConsume(queue: "gigacalc-input",
                        autoAck: true,
                        consumer: consumer);
                    
                    System.Threading.Thread.Sleep(1);
                    if (message != "")
                        Console.WriteLine(message);

                    HandleMessage(message);
                }
            }
        }

        static void HandleMessage(string message)
        {
            if (message.Length == 0)
                return;
            if (message.Length == 1)
            {
                _calcClass.Insert(message[0]);
            }
            else if (message[0] == 'M')
            {
                switch (message[1])
                {
                    case '+':
                        _calcClass.MemoryAdd();
                        break;
                    case '-':
                        _calcClass.MemorySub();
                        break;
                    case 'S':
                        _calcClass.MemorySave();
                        break;
                    case 'C':
                        _calcClass.MemoryClear();
                        break;
                    case 'L':
                        _calcClass.MemoryLoad();
                        break;
                }
            }
            else if (message.Split('-')[0] == "Swap")
            {
                try
                {
                    int bitNum = Int32.Parse(message.Split('-')[1]);
                    _calcClass.SwapBit(bitNum);
                }
                catch (Exception) { }
            }
            else if (message == "Rst")
            {
                _calcClass.Restart();
            }
            else if (message.Split('-')[0] == "System")
            {
                switch (message.Split('-')[1])
                {
                    case "bin":
                        _calcClass.CalcSystem = CalcSystems.SystemBin;
                        break;
                    case "oct":
                        _calcClass.CalcSystem = CalcSystems.SystemOct;
                        break;
                    case "dec":
                        _calcClass.CalcSystem = CalcSystems.SystemDec;
                        break;
                    case "hex":
                        _calcClass.CalcSystem = CalcSystems.SystemHex;
                        break;
                }
            }
            else if (message.Split('-')[0] == "Wordsize")
            {
                switch (message.Split('-')[1])
                {
                    case "8":
                        _calcClass.CalcDType = CalcDTypes.TypByte8;
                        break;
                    case "16":
                        _calcClass.CalcDType = CalcDTypes.TypeWWord16;
                        break;
                    case "32":
                        _calcClass.CalcDType = CalcDTypes.TypeDWord32;
                        break;
                    case "64":
                        _calcClass.CalcDType = CalcDTypes.TypeQWord64;
                        break;
                }
            }
            
            SendResults();
        }

        static void SendResults()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                string message = _calcClass.calcValuesInActiveSystem;
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "gigacalc-mainoutput",
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent {0} to main output", message);

                message = string.Join("", _calcClass.binArray);
                body = Encoding.UTF8.GetBytes(message);
                
                channel.BasicPublish(exchange: "",
                    routingKey: "gigacalc-binoutput",
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent {0} to bin output", message);

                switch (_calcClass.CalcSystem)
                {
                    case CalcSystems.SystemBin:
                        message = "bin";
                        break;
                    case CalcSystems.SystemOct:
                        message = "oct";
                        break;
                    case CalcSystems.SystemDec:
                        message = "dec";
                        break;
                    case CalcSystems.SystemHex:
                        message = "hex";
                        break;
                }
                
                body = Encoding.UTF8.GetBytes(message);
                
                channel.BasicPublish(exchange: "",
                    routingKey: "gigacalc-system",
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent {0} to system output", message);

                switch (_calcClass.CalcDType)
                {
                    case CalcDTypes.TypByte8:
                        message = "8";
                        break;
                    case CalcDTypes.TypeWWord16:
                        message = "16";
                        break;
                    case CalcDTypes.TypeDWord32:
                        message = "32";
                        break;
                    case CalcDTypes.TypeQWord64:
                        message = "64";
                        break;
                }
                
                body = Encoding.UTF8.GetBytes(message);
                
                channel.BasicPublish(exchange: "",
                    routingKey: "gigacalc-wordsize",
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent {0} to wordsize output", message);
            }
        }
    }
}