using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Text;


namespace FrameworkIdentify
{
    class Program
    {
        static readonly object _logLockObj = new object();
        private static StringBuilder stbLog = new StringBuilder();
        private static SystemInfo systemInfo = null;

        static void Main(string[] args)
        {

            Console.WriteLine(" Coletando informações, aguarde...");

            SystemInfo system = GetSystemInfo();

          
            Console.WriteLine(" ");
            stbLog.AppendLine(" ");

            Console.WriteLine("--- " + DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToShortTimeString() +"---- ");
            stbLog.AppendLine("-------- " + DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToShortTimeString() +" -----------");

            Console.WriteLine(" ");
            stbLog.AppendLine(" ");

            Console.WriteLine("Informação do Sistema: ");
            stbLog.AppendLine("Informação do Sistema: ");

            Console.WriteLine(" ");
            stbLog.AppendLine(" ");

            Console.WriteLine("Arquitetura: " + system.Archtecture.ToString());
            stbLog.AppendLine("Arquitetura: " + system.Archtecture.ToString());

            Console.WriteLine("CPU: " + system.CPU);
            stbLog.AppendLine("CPU: " + system.CPU);

            Console.WriteLine("Domínio: " + system.Domain);
            stbLog.AppendLine("Domínio: " + system.Domain);

            Console.WriteLine("Nome da máquina: " + system.HostName);
            stbLog.AppendLine("Nome da máquina: " + system.HostName);

            Console.WriteLine("Memória instalada: " + system.InstalledMemory);
            stbLog.AppendLine("Memória instalada: " + system.InstalledMemory);

            Console.WriteLine("Localização: " + system.Localization);
            stbLog.AppendLine("Localização: " + system.Localization);

            Console.WriteLine("Sistema Operacional: " + system.OSName);
            stbLog.AppendLine("Sistema Operacional: " + system.OSName);

            Console.WriteLine("Sistema Operacional Versão: " + system.OSVersion);
            stbLog.AppendLine("Sistema Operacional Versão: " + system.OSVersion);

            Console.WriteLine(" ");
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

        /// <summary>
		/// Obtém informações do sistema operacional
		/// </summary>
		/// <returns></returns>
		public static SystemInfo GetSystemInfo()
        {
            string sOutput = "";

            try
            {
                Process CurrentProcess = new Process();
                CurrentProcess.StartInfo.FileName = "systeminfo.exe";
                CurrentProcess.StartInfo.UseShellExecute = false;
                CurrentProcess.StartInfo.CreateNoWindow = true;
                CurrentProcess.StartInfo.RedirectStandardOutput = true;
                CurrentProcess.StartInfo.RedirectStandardInput = true;
                CurrentProcess.StartInfo.RedirectStandardError = true;
                CurrentProcess.Start();

                sOutput = CurrentProcess.StandardOutput.ReadToEnd();

                CurrentProcess.WaitForExit(10000);
                int exitCode = CurrentProcess.ExitCode;
                CurrentProcess.Close();

                sOutput = sOutput.Replace("\r", "");

                string[] strs = sOutput.Split(new char[] { '\n' });
                List<string> lines = new List<string>();
                for (int i = 0; i < strs.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strs[i]) && (strs[i][0] != ' '))
                        lines.Add(strs[i]);
                }

                int rColumnWidth = 20;
                for (int i = rColumnWidth; i < lines[0].Length; i++)
                {
                    if (lines[0][i] != ' ') break;
                    rColumnWidth++;
                }

                SystemInfo temp = new SystemInfo();
                temp.Domain = lines[28].Substring(rColumnWidth, lines[28].Length - rColumnWidth).Replace(".local", "");
                temp.HostName = lines[0].Substring(rColumnWidth, lines[0].Length - rColumnWidth);
                temp.Localization = lines[19].Substring(rColumnWidth, lines[19].Length - rColumnWidth);
                temp.OSName = lines[1].Substring(rColumnWidth, lines[1].Length - rColumnWidth);
                temp.OSVersion = lines[2].Substring(rColumnWidth, lines[2].Length - rColumnWidth);
                if (lines[13].Substring(rColumnWidth, lines[13].Length - rColumnWidth).Contains("64"))
                    temp.Archtecture = SystemArchtecture.x64;
                else
                    temp.Archtecture = SystemArchtecture.x86;

                temp.InstalledMemory = lines[22].Substring(rColumnWidth, lines[22].Length - rColumnWidth);
                temp.CPU = GetProcessorID();
                temp.IsWin2003EnterpriseEditionX86 = temp.OSName.Equals("Microsoft(R) Windows(R) Server 2003, Enterprise Edition") && (temp.Archtecture == SystemArchtecture.x86);
                systemInfo = temp;
            }
            catch
            {
               
            }

            return systemInfo;
        }

        private static string GetProcessorID()
        {
            try
            {
                string sCpuInfo = String.Empty;

                //*** Declare Management Class
                ManagementClass clsMgtClass = new ManagementClass("Win32_Processor");
                ManagementObjectCollection colMgtObjCol = clsMgtClass.GetInstances();

                //*** Loop Over Objects
                foreach (ManagementObject objMgtObj in colMgtObjCol)
                {
                    //*** Only return cpuInfo from first CPU
                    if (sCpuInfo == String.Empty)
                    {
                        sCpuInfo = objMgtObj.Properties["Name"].Value.ToString();
                    }
                }

                return sCpuInfo;
            }
            catch
            {
                return "unknown";
            }
        }
    }

    /// <summary>
    /// classe representa informações do sistema
    /// </summary>
    public class SystemInfo
    {
        public string HostName;
        public string OSName;
        public string OSVersion;
        public SystemArchtecture Archtecture;
        public string Localization;
        public string Domain;
        public string InstalledMemory;
        public string CPU;
        public bool IsWin2003EnterpriseEditionX86;
    }

    /// <summary>
    /// Enumerador define arquitetura do sistema
    /// </summary>
    public enum SystemArchtecture
    {
        x86,
        x64
    }

}
