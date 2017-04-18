using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FrameworkIdentify
{
    class Program
    {
        static readonly object _logLockObj = new object();
        private static StringBuilder stbLog = new StringBuilder();

        static void Main(string[] args)
        {
            stbLog.AppendLine(" ");
            stbLog.AppendLine("-------- " + DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToShortTimeString() +" -----------");
            stbLog.AppendLine(" ");
            Console.WriteLine("Versões .NET Framework instalados: ");
            stbLog.AppendLine("Versões .NET Framework instalados: ");

            Console.WriteLine();
            stbLog.AppendLine(" ");

            //Abre os registros do .NET Framework.
            using (RegistryKey ndpKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {                
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {

                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", "");
                        string sp = versionKey.GetValue("SP", "").ToString();
                        string install = versionKey.GetValue("Install", "").ToString();
                        if (install == "")
                        {     
                            foreach (string subKeyName in versionKey.GetSubKeyNames())
                            {
                                RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                                name = (string)subKey.GetValue("Version", "");
                                if (name != "")
                                    sp = subKey.GetValue("SP", "").ToString();
                                install = subKey.GetValue("Install", "").ToString();
                                string release = subKey.GetValue("Release", "").ToString();
                                if (install == "")
                                {
                                    Console.WriteLine(versionKeyName + "  " + name);
                                    stbLog.AppendLine(versionKeyName + "  " + name);
                                }                                    
                                else
                                {
                                    if (release !="")
                                    {
                                        switch (release)
                                        {
                                            case "378389":
                                                Console.WriteLine(".NET Framework 4.5 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.5 - " + subKeyName);
                                                break;
                                            case "378675": //.NET Framework 4.5.1 installed with Windows 8.1 or Windows Server 2012 R2
                                                Console.WriteLine(".NET Framework 4.5.1 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.5.1 - " + subKeyName);
                                                break;
                                            case "378758": //.NET Framework 4.5.1 installed on Windows 8, Windows 7 SP1, or Windows Vista SP2
                                                Console.WriteLine(".NET Framework 4.5.1 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.5.1 - " + subKeyName);
                                                break;
                                            case "379893":
                                                Console.WriteLine(".NET Framework 4.5.2 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.5.2 - " + subKeyName);
                                                break;
                                            case "393295": //On Windows 10 systems: 393295
                                                Console.WriteLine(".NET Framework 4.6 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.6 - " + subKeyName);
                                                break;
                                            case "393297": //On all other OS versions: 393297
                                                Console.WriteLine(".NET Framework 4.6 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.6 - " + subKeyName);
                                                break;
                                            case "394254": //On Windows 10 November Update systems: 394254
                                                Console.WriteLine(".NET Framework 4.6.1 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.6.1 - " + subKeyName);
                                                break;
                                            case "394271": //On all other OS versions: 394271
                                                Console.WriteLine(".NET Framework 4.6.1 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.6.1 - " + subKeyName);
                                                break;
                                            case "394802": //On Windows 10 Anniversary Update: 394802
                                                Console.WriteLine(".NET Framework 4.6.2 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.6.2 - " + subKeyName);
                                                break;
                                            case "394806": //On all other OS versions: 394806
                                                Console.WriteLine(".NET Framework 4.6.2 - " + subKeyName);
                                                stbLog.AppendLine(".NET Framework 4.6.2 - " + subKeyName);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (sp != "" && install == "1")
                                        {
                                            Console.WriteLine(".NET Framework " + subKeyName + "  " + name + "  SP" + sp);
                                            stbLog.AppendLine(".NET Framework " + subKeyName + "  " + name + "  SP" + sp);
                                        }
                                        else if (install == "1")
                                        {
                                            if (versionKeyName.Equals("v4.0"))
                                            {
                                                Console.WriteLine(".NET Framework 4.0 - " + subKeyName + " - " + name);
                                                stbLog.AppendLine(".NET Framework 4.0 - " + subKeyName + " - " + name);
                                            }                                                
                                            else
                                            {
                                                Console.WriteLine("  " + subKeyName + "  " + name);
                                                stbLog.AppendLine("  " + subKeyName + "  " + name);
                                            }                                                
                                        }
                                    }                                   
                                }
                            }
                        }                            
                        else
                        {
                            if (sp != "" && install == "1")
                            {
                                Console.WriteLine(".NET Framework " + versionKeyName + " - " + name + "  SP" + sp);
                                stbLog.AppendLine(".NET Framework " + versionKeyName + " - " + name + "  SP" + sp);
                            }

                        }
                    }
                }
            }

            Console.WriteLine();
            Exportar();
            Console.WriteLine();
            Console.ReadKey();
        }

        private static void Exportar()
        {
            string path = Environment.CurrentDirectory;
            EscreverArquivoLog(path);
        }

        /// <summary>
        /// Método escreve o log
        /// </summary>
        private static void EscreverArquivoLog(string appPath)
        {            
            string fileName = Path.Combine(appPath, "Versoes_Framework_Instalados.txt");            

            lock (_logLockObj)
            {
                TextWriter tw = StreamWriter.Synchronized(File.AppendText(fileName));
                
                try
                {
                    //exporta                 
                    tw.Write(stbLog.ToString());
                    //limpa o stringbuilder
                    stbLog.Remove(0, stbLog.Length);
                    Process.Start(fileName);
                    Console.WriteLine("Resultado exportado com sucesso em: " + fileName);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    tw.Close();
                    tw.Dispose();
                }
            }
        }
    }
}
