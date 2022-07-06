using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace TrabajandoAPI
{
    partial class Program
    {

        static void Main(string[] args)
        {
            GetAOE();
            //GetClima();
            Console.WriteLine();
            Console.WriteLine("FIN.");
        }


        private static void GetAOE()
        {
            var url = @"https://age-of-empires-2-api.herokuapp.com/api/v1/civilizations";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            const string NombreArchivo = "civiliz.json";
                            var miHelperdeArchivos = new HelperDeJson();
                            string responseBody = objReader.ReadToEnd();
                            Root ListCivRec = JsonSerializer.Deserialize<Root>(responseBody);
                            //Civilization ListCivilizacion = JsonSerializer.Deserialize<Civilization>(responseBody);
                            foreach (Civilization item in ListCivRec.civilizations)
                            {
                                Console.WriteLine("id: " + item.id + " Nombre: " + item.name);
                            }
                            System.Console.WriteLine("Eliga el ID de la civilizacion para ver sus datos");
                            int idciv=Convert.ToInt32(Console.ReadLine());
                            foreach (Civilization item in ListCivRec.civilizations)
                            {
                                if (idciv==item.id)
                                {
                                    item.MostrarC(item);
                                }
                            }
                            //Guardo el archivo
                            Console.WriteLine();
                            Console.WriteLine("--Serializando--");
                            string CJson = JsonSerializer.Serialize(ListCivRec);
                            Console.WriteLine("Archivo Serializado : " + ListCivRec);
                            Console.WriteLine("--Guardando--");
                            miHelperdeArchivos.GuardarArchivoTexto(NombreArchivo, CJson);

                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API");
            }
        }

        // private static void GetClima()
        // {
        //    var url = $"https://ws.smn.gob.ar/map_items/weather/";
        //     var request = (HttpWebRequest)WebRequest.Create(url);
        //     request.Method = "GET";
        //     request.ContentType = "application/json";
        //     request.Accept = "application/json";
           
        //     try
        //     {
        //         using (WebResponse response = request.GetResponse())
        //         {
        //             using (Stream strReader = response.GetResponseStream())
        //             {
        //                 if (strReader == null) return;
        //                 using (StreamReader objReader = new StreamReader(strReader))
        //                 {
        //                     string responseBody = objReader.ReadToEnd();
        //                     List<Root> listclima = JsonSerializer.Deserialize<List<Root>>(responseBody);
        //                     foreach (var Prov in listclima)
        //                     {
        //                         Console.WriteLine("Nombre: " + Prov.name + " Temperatura: " + Prov.weather.temp);
        //                     }

        //                 }
        //             }
        //         }
        //     }
            // catch (WebException ex)
            // {
            //     Console.WriteLine("Problemas de acceso a la API");
            // }
        // }

    }
}
