using System;
using System.Collections.Generic;
using System.Text;
using SmsApi;

namespace SMS_Send
{
    class Program
    {
        static void Main(string[] args)
        {
            Sms sms = new Sms("HRSaveTime", "713ed5aad516f", true);
            while (true)
            {
                string result = Console.ReadLine();
                switch (result)
                {
                    case "getBalance":
                        {
                            // getBalance() Запрос баланса
                            ResponseBalance rBalance = sms.getBalance();
                            if (rBalance.status == "success")
                                Console.WriteLine(rBalance.balance); // покажем текущий баланс
                            else
                                Console.WriteLine(rBalance.message); // иначе выведем сообщение об ошибке
                            break;
                        }
                    case "getMessagesPrice":
                        {
                            // getMessagesPrice(sender, recipients, message) запрос стоимости отправки сообщения на указанные номера
                            // Параметры sender - имя отправителя.
                            // Параметры recipients - номера получателей в любом формате через запятую. message - текст сообщения.
                            ResponsePrice rprice = sms.getMessagesPrice("sendertest", "89535143056", "Test");
                            if (rprice.status == "success")
                                Console.WriteLine(rprice.price); // покажем стоимость отправки сообщения на указанные номера
                            else
                                Console.WriteLine(rprice.message); // иначе выведем сообщение об ошибке
                            break;
                        }

                    case "sendMessage":
                        {
                            //send(sender, recipients, message, run_at) - отправка сообщения
                            // Параметры sender - имя отправителя. recipients - номера получателей в любом формате через запятую. message - текст сообщения.
                            // Параметры run_at - дата и время отправки для отправки запланированного сообщения, формат "ДД.ММ.ГГГГ ЧЧ:ММ:СС", например 25.04.2016 10:00
                            ResponseSend rsend = sms.send("sendertest", "89535143056", "test");
                            if (rsend.status == "success")
                                Console.WriteLine("ok"); // если сообщение было успешно отправленно выведем ok
                            else
                                Console.WriteLine(rsend.message); // иначе выведем сообщение об ошибке
                            break;    
                        }

                    case "getMessagesStatus":
                        {
                            // getMessagesStatus(messages_id) - запрос статуса сообщений
                            // messages_id - id сообщений через запятую
                            ResponseStatus rstatus = sms.getMessagesStatus("119,118");
                            if (rstatus.status == "success")
                                Console.WriteLine("119 - " + rstatus.messages["119"] + "; 118 - " + rstatus.messages["118"]); // Выводим ид сообщения и статус их доставки
                            else
                                Console.WriteLine(rstatus.message); // иначе выведем сообщение об ошибке
                            break;
                        }

                    case "cancelSms":
                        {
                            // cancelSms(messages_id) - отмена запланированных сообщений
                            // messages_id - id сообщений через запятую
                            ResponseCancel rstatus1 = sms.cancelSms("119,118");
                            if (rstatus1.status == "success")
                                Console.WriteLine("119 - " + rstatus1.messages["119"] + "; 118 - " + rstatus1.messages["118"]); // Выводим ид сообщения и статус
                            else
                                Console.WriteLine(rstatus1.message); // иначе выведем сообщение об ошибке
                            break;
                        }

                    case "getPhonesInfo":
                        {
                            //info(phones) - запрос информации по номерам
                            ResponseInfo rinfo = sms.getPhonesInfo("89535143056");
                            if (rinfo.status == "success")
                            {
                                foreach (PhoneInfo pi in rinfo.info)
                                {
                                    Console.WriteLine("Номер - " + pi.phone + "; Оператор - " + pi.name + "; Регион - " + pi.region);
                                }
                            }
                            else
                                Console.WriteLine(rinfo.message);
                            break;
                        }
                }


               

                

                

                

                
            }
        }
    }
}
