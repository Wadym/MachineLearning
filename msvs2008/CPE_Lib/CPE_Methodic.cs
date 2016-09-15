using System;
using System.Collections.Generic;
using System.Text;

namespace CPE_Lib
{
    public class CPE_Methodic
    {
        /// <summary>
        /// Начальная передача параметров.
        /// </summary>
        /// <param name="data">объект данных</param>
        public virtual void Config(string data)
        {
        }
        /// <summary>
        /// Начальная инициализация. Метод запускается один раз. В самом начале.
        /// </summary>
        public virtual void Init()
        {
        }
        /// <summary>
        /// Активация блока. Данный метод вызывается перед тем, как начать вызываьт Execute
        /// </summary>
        public virtual void Activate()
        {
        }
        /// <summary>
        /// Метод обработки. Данный метод запускается автоматом. В метод передается массив входных данных.
        /// </summary>
        /// <param name="data"></param>
        public virtual void Execute(Slice data)
        {
        }
        /// <summary>
        /// Метод запускается при паузе.
        /// </summary>
        public virtual void DeActivate()
        {
        }
        /// <summary>
        /// Запускается перед выходом из программы
        /// </summary>
        public virtual void DeInit()
        {
        }
        static Dictionary<string, Type> avalible_methodics = new Dictionary<string, Type>();
        public static Dictionary<string, Type> GetMethodics()
        {
            return avalible_methodics;
        }
    }
}
