using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

using Genius;
using Genius.Core;
using Genius.Clients;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Data.Common;
using System.Drawing;
using Aspose.Imaging;
using Genius.Models.Artist;
using static System.Net.Mime.MediaTypeNames;



namespace MutabotApp
{

    internal class Program
    {
        static bool key = false;
        enum Status { Normal, StartTag, ParPoint, EndPoint, EndParPoint, Num, EndHTag, EndHeader, EndNum }

        public static string parse(string text)
        {
            string buff = "";
            bool flag = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (!flag)
                {
                    if (text[i] == '<') { flag = true; continue; }
                    buff += text[i];

                }
                else
                {
                    if (flag)
                    {
                        if (text[i] == '>')
                        {
                            flag = false;
                        }

                    }
                }
            }

            return buff;
        }
        //public static string parseP(string text) 
        //{
        //    string buff = "";
        //    char symb, indx = ' ';
        //    int i = 0;
        //    Status status = Status.Normal;
        //    while (i < text.Length)
        //    {
        //        symb = text[i];
        //        switch (status) 
        //        { 
        //        case Status.Normal:
        //            if(symb == '<')
        //                {
        //                    status = Status.StartTag;
        //                    break;
        //                }
        //                buff += symb;
        //                break;
        //        case Status.StartTag:
        //            if(symb == 'p')
        //                {
        //                    status = Status.ParPoint; 
        //                    break;
        //                }
        //            else if(symb == 'h')
        //                {
        //                    status = Status.Num; 
        //                    break;
        //                }
        //            else if(symb == '/')
        //                {
        //                    status = Status.EndPoint;
        //                    break;
        //                }
        //            status = Status.Normal;
        //            buff += "<" + symb;
        //            break;
        //        case Status.ParPoint:
        //            if(symb == '>')
        //                {
        //                    status = Status.Normal;
        //                    break;
        //                }
        //            status = Status.Normal;
        //            buff += "<" + "p" + symb;
        //            break;
        //        case Status.EndPoint:
        //            if(symb == 'p')
        //                {
        //                    status = Status.EndParPoint;
        //                    break;
        //                }
        //            if(symb == 'h')
        //                {
        //                    status = Status.EndHTag;
        //                    break;
        //                }
        //            status = Status.Normal;
        //            buff += "</" + symb;
        //            break;
        //        case Status.EndParPoint:
        //            if (symb == '>')
        //                {
        //                    status = Status.Normal;
        //                    break;
        //                }
        //            status = Status.Normal;
        //                buff += "</p" + symb;
        //            break;
        //        case Status.Num:
        //            if(symb == '1' || symb == '2' || symb == '3' || symb == '4' || symb == '5' || symb == '6')
        //                {
        //                    status = Status.EndHTag;
        //                    indx = symb;
        //                    break;
        //                }
        //            status = Status.Normal;
        //            buff += "<" + "h" + symb;
        //            break;
        //        case Status.EndNum:
        //            if(symb == '>')
        //                {
        //                    status = Status.Normal;
        //                    break ;
        //                }

        //            status = Status.Normal;
        //            buff += "<" + indx + symb;
        //            break;
        //        case Status.EndHeader:
        //                if (symb == '>')
        //                {
        //                    status = Status.Normal ;
        //                    break ;
        //                }
        //                status = Status.Normal;
        //                buff += "<h" + indx + symb;
        //                break;
        //        case Status.EndHTag:
        //                if (symb == '1' || symb == '2' || symb == '3' || symb == '4' || symb == '5' || symb == '6')
        //                {
        //                    status = Status.EndNum;
        //                    indx = symb;
        //                    break;
        //                }
        //                status = Status.Normal;
        //                buff += "</" + "h" + symb;
        //                break;
        //        }
        //        i++;

        //    }
        //    return buff;
        //}



        static void Main(string[] args)
        {
            //message
            Console.WriteLine("Programm Started!");

            var botClient = new TelegramBotClient("7546641008:AAEV5CWO3Td3fK9wmkKIeTg_tWuNu6Ciz-w");
            botClient.StartReceiving(Update, Error);

            //waiting
            Console.ReadLine();
        }

        private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var gClient = new GeniusClient("SMxCfvYk_QNdaiyY8rvJ00WaA0MU5A5S-N00WsD_I4gPZWv8aBeYvEJMoHs0w8w6");





            var message = update.Message;

            if (message.Text != null)
            {
                Console.WriteLine($"{message.Chat.FirstName} | {message.Text}");
                if (message.Text.ToLower().Contains("start"))
                {

                    await client.SendTextMessageAsync(message.Chat.Id, "Привет🥰, меня зовут mutabot😀 и я расскажу тебе актуальную информацию об артистах💪! Чтобы узнать список команд напиши /help🤯");
                    return;
                }
                else if (message.Text.ToLower().Contains("help"))
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "/спортсменыЗдесь? - ответит мемом😎\n (имя артиста) - краткая информация об артисте🔥\n скинь фото и получишь красивую обработанную картинку❤️‍🔥");
                    return;
                }

                else if (message.Text.ToLower().Contains("спортсмены Здесь?"))
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "Все спортсмены на месте");
                }

                else if (message.Text != null)
                {
                    try
                    {


                        var tmp = gClient.SearchClient.Search(message.Text.Replace(" ", "%20"));
                        if (tmp != null)
                        {
                            var msc = tmp.Result.Response.Hits.FirstOrDefault();
                            if (msc != null)
                            {
                                var artistId = msc.Result.PrimaryArtist.Id;
                                var artist = gClient.ArtistClient.GetArtist(artistId);
                                if (artist != null)
                                {

                                    var description = artist.Result.Response.Artist.Description;
                                    var text = parse(artist.Result.Response.Artist.Description.Html);
                                    var tp = await client.SendPhotoAsync(message.Chat.Id, artist.Result.Response.Artist.ImageUrl, caption: text);
                                }
                            }

                        }
                        else { await client.SendTextMessageAsync(message.Chat.Id, "артиста нет в базе данных(("); }
                    }
                    catch (Exception ex) { }

                }







            }
            if (message.Document != null)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "сейчас будет хайп");

                var field = update.Message.Document.FileId;
                if (field != null&& !(field.Contains(".png")||field.Contains(".jpg")||field.Contains(".jpeg")))
                {
                    var fileInfo = await client.GetFileAsync(field);
                    var filePath = fileInfo.FilePath;

                    string destinationFilePath = $"C:\\Users\\Boris\\Desktop\\мобильные устройства\\trash\\{message.Document.FileName}";
                    await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                    await client.DownloadFileAsync(filePath, fileStream);


                    fileStream.Close();

                    var image = Aspose.Imaging.Image.Load(destinationFilePath);
                    image.Resize(300, 300, ResizeType.LanczosResample);
                    string newName = $"C:\\Users\\Boris\\Desktop\\мобильные устройства\\trash\\edited";
                    image.Save(newName);

                    await using Stream stream = System.IO.File.OpenRead(newName);
                    await client.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(stream, message.Document.FileName.Replace(".", "_edited.")));
                    stream.Close();
                    return;
                }
                await client.SendTextMessageAsync(message.Chat.Id, "хайпа не будет, нужно скинуть в формате .png, .jpg или .jpeg");
                return;
            }
            //fidler
        }
    }
}
