using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Log
{
    public enum Capa { UI, Server, Entitie };
    public class EngineLog
    {
        protected static readonly ILog fileLogger = LogManager.GetLogger("FileLogger");
        protected static readonly ILog eventLogger = LogManager.GetLogger("EventLogger");       
        string ALERT = log4net.Core.Level.Alert.DisplayName.ToString();
        string DEBUG = log4net.Core.Level.Debug.Name.ToString();
        string INFO = log4net.Core.Level.Info.ToString();
        string EMERGENCY = log4net.Core.Level.Fatal.ToString();
        string VERBOSE = log4net.Core.Level.Verbose.ToString();
        public Capa capa;
        public object entidad=null;

        

        public enum LevelError { ALERT, DEBUG , INFO , EMERGENCY , VERBOSE,FINE,ERROR };
        public EngineLog(Capa capa, object entidad=null)
        {
            this.capa = capa;
            this.entidad = entidad;
           // log4net.Config.XmlConfigurator.Configure();
          

        }

        public void writeFile(string msg, LevelError level)
        {
            switch (level.ToString())
            {
                case "ALERT": fileLogger.Error(msgString(msg)); break;
                case "DEBUG": fileLogger.Debug(msgString(msg)); break;
                case "EMERGENCY": fileLogger.Fatal(msgString(msg)); break;
                case "INFO": fileLogger.Info(msgString(msg)); break;
                case "VERBOSE": fileLogger.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            log4net.Core.Level.Trace, msgString(msg), null); break;

            }
        }

        public void writeBD(string msg, LevelError level)
        {
            switch (level.ToString())
            {                
                case "ALERT": eventLogger.Error(msgString(msg)); break;
                case "DEBUG": eventLogger.Debug(msgString(msg)); break;
                case "EMERGENCY": eventLogger.Fatal(msgString(msg)); break;
                case "INFO": eventLogger.Info(msgString( msg)); break;
                case "VERBOSE": eventLogger.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            log4net.Core.Level.Trace, msgString(msg), null); break;

             

            }
        }

        public void writeBoth(string msg, LevelError level)
        {
            writeFile(msg, level);
            writeBD(msg, level);

        }

        private string msgString(string msg)
        {
            return string.Format("[{0}/{2}]-{1}", this.capa.ToString(), msg, this.entidad!=null?this.entidad.GetType().Name:string.Empty);
        }

       
    }
}