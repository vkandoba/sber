using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LowLevel;
using PluginCore;
using PluginCore.Link;
using PluginCore.Load;

namespace Plugin_Mtk
{
    public class HandlerClass : PluginInterface.IPlugin
    {
        public static int globalCounter = 0;
        private ContentLoader contentLoader;
        private ObjectLinkMiner objectLinkMiner;

        public HandlerClass()
        {
            contentLoader = new ContentLoader();
            objectLinkMiner = new ObjectLinkMiner();
        }

        public object pluginHandler(Dictionary<string, object> parameters, out string error)
        {
            try
            {
                error = "";
                string type = parameters["type"].ToString();

                #region load_page_plugin (плагин загрузки страницы)

                if (extra.sc(type, "load_page_plugin"))
                {
                    string url = parameters["url"].ToString();

                    string content;
                    if (url.Contains("list"))
                    {
                        var http = (DatacolHttp)parameters["datacolhttp"];
                        string referer = parameters["referer"].ToString();
                        var outParams = new Dictionary<string, object>();
                        PluginConfigrator.UnsafeHeaderParsingOn();
                        try
                        {
                            content = http.request(url, referer, out outParams, out error);
                        }
                        finally
                        {
                            PluginConfigrator.UnsafeHeaderParsingOff();
                        }
                    }
                    else
                    {
                        content = contentLoader.GetListContent(url, 1);
                    }
                    return content;
                }
                #endregion

                #region data_area_gather_plugin (плагин сбора диапазонов с данными)
                if (extra.sc(type, "data_area_gather_plugin"))
                {
                    List<string> resultingList = new List<string>();

                    //параметр ССЫЛКА на страницу, на которой происходит сбор диапазонов
                    string url = parameters["url"].ToString();
                    //параметр реферер для страницы, на которой происходит сбор диапазонов
                    string referer = parameters["referer"].ToString();
                    //параметр исходный код реферера для страницы, на которой происходит сбор диапазонов
                    string referer_pagecode = parameters["referer_pagecode"].ToString();
                    //контент страницы, на которой происходит сбор диапазонов
                    string content = parameters["content"].ToString();


                    //пример. удалить или изменить
                    //resultingList.Add("pagecode_");

                    //возвращает список диапазонов для сбора данных
                    return resultingList;
                }
                #endregion

                #region data_gather_plugin (плагин сбора данных (или плагин обработки собранных данных))
                if (extra.sc(type, "data_gather_plugin"))
                {
                    //параметр найденное значение поля данных, обработка которого производится (в него также 
                    //может быть передан исходный код диапазона или страницы, чтобы там был проведен поиск самого значения
                    string value = parameters["value"].ToString();
                    //параметр URL текущей страницы
                    string url = parameters["url"].ToString();
                    //параметр реферер текущей страницы
                    string referer = parameters["referer"].ToString();
                    //параметр исходный код реферера текущей страницы
                    string referer_pagecode = parameters["referer_pagecode"].ToString();
                    //параметр имя текущего поля данных
                    string fieldname = parameters["fieldname"].ToString();
                    //параметр ряд данных, в котором передаются значения текущей группы данных (заполнены только значения полей
                    //расположенных в списке полей выше, чем текущее поле)
                    DataRow dr = (DataRow)parameters["datarow"];

                    #region Get Config Params
                    //параметр конфигурация плагина экспорта
                    string config = parameters["config"].ToString(); 

                    Dictionary<string, object> configParams = GetDictionaryParamsConfig(config);

                    //примеры параметров
                    //
                    //List<string> listParameter = (List<string>)configParams["list-parameter"];
                    //bool boolParameter =  Convert.ToInt32(configParams["bool-parameter"].ToString()) == 1;
                    //int intParameter = Convert.ToInt32(configParams["int-parameter"].ToString());
                    //string stringParameter = configParams["string-parameter"].ToString();
                    #endregion

                    //Преобразовывается значение как необходимо
                    //пример. удалить или изменить
                    string newvalue = value + " Test ";

                    //возвращает обработанное/найденное значение поля
                    return newvalue;
                }
                #endregion

                #region load_files_plugin (плагин загрузки файлов)
                if (extra.sc(type, "load_files_plugin"))
                {
                    List<string> resultingList = new List<string>();

                    //параметр найденное значение поля данных
                    string value = parameters["value"].ToString();
                    //параметр URL текущей страницы
                    string url = parameters["url"].ToString();
                    //параметр имя текущего поля данных
                    string fieldname = parameters["fieldname"].ToString();
                    //параметр папка для сохранения файлов
                    string folder = parameters["folder"].ToString();
                    //параметр ряд данных, в котором передаются значения текущей группы данных (заполнены только значения полей
                    //расположенных в списке полей выше, чем текущее поле)
                    DataRow dr = (DataRow)parameters["datarow"];

                    //пример. удалить или изменить
                    //resultingList.Add("c:\\");

                    //возвращает список локальных путей загруженных файлов
                    return resultingList;
                }
                #endregion

                #region links_gather_plugin (плагин сбора ссылок)
                if (extra.sc(type, "links_gather_plugin"))
                {
                    string content = parameters["content"].ToString();
//                    File.AppendAllLines(log, objectLinkMiner.Extract(content));
                    return new HashSet<string>(objectLinkMiner.Extract(content));
                }
                #endregion

                #region pre_export_plugin (плагин преэкспорта)
                if (extra.sc(type, "pre_export_plugin"))
                {
                    //параметр URL страницы, на которой собраны данные
                    string url = parameters["url"].ToString();
                    //параметр таблица собранных данных (содержит все собранные с текущей страницы группы данных)
                    DataTable dt = (DataTable)parameters["datatable"];
                    //параметр список названий колонок таблицы собранных данных
                    List<string> columnNames = (List<string>)parameters["columnnames"];



                    //Возвращает флаг возможности экспорта собранных данных (если возвращается false, экспорт не будет проивзодиться)
                    return false;
                }
                #endregion

                #region export_plugin (плагин экспорта)
                if (extra.sc(type, "export_plugin"))
                {
                    //параметр URL страницы, на которой собраны данные
                    string url = parameters["url"].ToString();
                    
                    //параметр таблица собранных данных (содержит все собранные с текущей страницы группы данных)
                    DataTable dt = (DataTable)parameters["datatable"];
                    //параметр список названий колонок таблицы собранных данных
                    List<string> columnNames = (List<string>)parameters["columnnames"];


                    #region Get Config Params
                    //параметр конфигурация плагина экспорта
                    string config = parameters["config"].ToString();

                    Dictionary<string, object> configParams = GetDictionaryParamsConfig(config);

                    //List<string> listParameter = (List<string>)configParams["list-parameter"];
                    //bool boolParameter =  Convert.ToInt32(configParams["bool-parameter"].ToString()) == 1;
                    //int intParameter = Convert.ToInt32(configParams["int-parameter"].ToString());
                    //string stringParameter = configParams["string-parameter"].ToString();
                    #endregion

                    string text = "";
                    //пример. удалить или изменить
                    //В цикле проходим по строке данных 
                    foreach (DataRow dr in dt.Rows)
                    {
                        //По названию получаем значение поля content
                        text = dr["content"].ToString();

                        //проходим по списку названий колонок для получения данных из них
                        foreach (string columnName in columnNames)
                        {
                            //в поле текст записываем данные получая значения из dr[columnName]
                            text = text.Replace("%" + columnName + "%", dr[columnName].ToString());
                        }
                        try
                        {
                            //пример. удалить или изменить
                            //Записываем в файл testfile.txt данные.
                            extra.WriteToFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "testfile.txt"),
                                    "\r\n====url = " + url + "==config=" + config + "==\r\n" + text + "\r\n========\r\n");
                        }
                        catch (Exception ex)
                        {
                            error = ex.GetBaseException().Message;
                        }
                    }
                    //возвращаемое значение не используется
                }
                #endregion

                #region finalize_plugin (плагин финализации)
                if (extra.sc(type, "finalize_plugin"))
                {
                    //Используется для обнуления счетчиков, закрытия соединений с базой данных, удаления временных файлов ...    
                    globalCounter = 0;

                    //возвращаемое значение не используется
                }
                #endregion
            }
            catch (Exception exp)
            {
                error = $"{exp.Message}\n{exp.StackTrace}";
            }

            return "возвращаемое значение по умолчанию (для типов плагинов, у которых оно не используется)";
        }


        #region Plugin Specific Functions
       
        #endregion

        #region Служебные функции


        public static Dictionary<string, object> GetDictionaryParamsConfig(string config)
        {

            string filecontent = config;
            Dictionary<string, object> dictParams = new Dictionary<string, object>();
            try
            {
                MatchCollection parameters = Regex.Matches(filecontent, "<dc5par[^<>]*?type=\"([^<>]*?)\"[^<>]*?name=\"([^<>]*?)\"[^<>]*?>(.*?)</dc5par>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                string type = string.Empty;
                string name = string.Empty;
                string value = string.Empty;

                int paramInt = -1;

                List<string> paramList = new List<string>();

                foreach (Match param1 in parameters)
                {


                    type = param1.Groups[1].Value.Trim();
                    name = param1.Groups[2].Value.Trim();
                    value = param1.Groups[3].Value.Trim();
                    if (type == "list-string")
                    {

                        paramList = extra.GetAllLines(value, true);

                        dictParams.Add(name, paramList);
                    }
                    else if (type == "string")
                    {
                        dictParams.Add(name, value);
                    }
                    else if (type == "int")
                    {
                        paramInt = Convert.ToInt32(value);
                        dictParams.Add(name, paramInt);
                    }
                    else
                    {
                        throw new Exception("Тип параметра " + type + " в конфигурации не поддерживается");
                    }

                }

            }
            catch
            {

            }

            return dictParams;
        }


        public static Dictionary<string, object> GetDictionaryParams(string filename, string encoding = "")
        {
            Encoding encode = null;

            if (encoding == "")
            {
                encode = Encoding.Default;
            }
            else
            {
                encode = Encoding.GetEncoding(encoding);
            }
            string filecontent = string.Empty;
            Dictionary<string, object> dictParams = new Dictionary<string, object>();
            if (!File.Exists(filename))
            {
                return dictParams;
            }
            else
            {
                filecontent = File.ReadAllText(filename, encode);

                MatchCollection parameters = Regex.Matches(filecontent, "<dc5par[^<>]*?type=\"([^<>]*?)\"[^<>]*?name=\"([^<>]*?)\"[^<>]*?>(.*?)</dc5par>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                string type = string.Empty;
                string name = string.Empty;
                string value = string.Empty;

                int paramInt = -1;

                List<string> paramList = new List<string>();

                foreach (Match param1 in parameters)
                {


                    type = param1.Groups[1].Value.Trim();
                    name = param1.Groups[2].Value.Trim();
                    value = param1.Groups[3].Value.Trim();
                    if (type == "list-string")
                    {

                        paramList = extra.GetAllLines(value, true);

                        dictParams.Add(name, paramList);
                    }
                    else if (type == "string")
                    {
                        dictParams.Add(name, value);
                    }
                    else if (type == "int")
                    {
                        paramInt = Convert.ToInt32(value);
                        dictParams.Add(name, paramInt);
                    }
                    else
                    {
                        throw new Exception("Тип параметра " + type + " в файле конфигурации " + filename + "не поддерживается");
                    }

                }

            }
            return dictParams;
        }

        #endregion

        #region Методы и свойства необходимые, для соответствия PluginInterface

        public void Init()
        {
        }

        public void Destroy()
        {
        }

        public string Name
        {
            get { return "Mtk"; }
        }

        public string Description
        {
            get { return "Парсер http://www.mtk.ru/"; }
        }

        #endregion
    }
}
