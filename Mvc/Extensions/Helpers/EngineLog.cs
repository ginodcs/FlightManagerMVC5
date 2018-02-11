using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace ARQ.Maqueta.Presentation.Mvc.Extensions.Helpers
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

        public void writeFile(Capa capa,string msg,log4net.Core.Level level)
        {
            switch(level.ToString())
            {
                case "ALERT": fileLogger.Error(msgString(capa,msg)); break;
                case "DEBUG": fileLogger.Debug(msgString(capa,msg)); break;
                case "EMERGENCY": fileLogger.Fatal(msgString(capa,msg)); break;
                case "INFO": fileLogger.Info(msgString(capa,msg)); break;
                case "VERBOSE": fileLogger.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, 
            log4net.Core.Level.Trace,msgString(capa,msg),null);break;
                
            }
        }
        
        public void writeBD(Capa capa,string msg,log4net.Core.Level level)
        {
            switch(level.ToString())
            {
                case "ALERT": eventLogger.Error(msgString(capa,msg)); break;
                case "DEBUG": eventLogger.Debug(msgString(capa,msg)); break;
                case "EMERGENCY": eventLogger.Fatal(msgString(capa,msg)); break;
                case "INFO": eventLogger.Info(msgString(capa,msg)); break;
                case "VERBOSE": eventLogger.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
            log4net.Core.Level.Trace, msgString(capa,msg),null); break;
                
            }
        }

        public void writeBoth(Capa capa,string msg, log4net.Core.Level level)
        {
            writeFile(capa,msg,level);
            writeBD(capa,msg,level);

        }

        private string msgString(Capa capa,string msg)
        {
            return string.Format("[{0}]-{1}",capa.ToString(),msg );
        }
      
    }
}