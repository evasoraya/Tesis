using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trinity;
using Trinity.Storage;

namespace Friends.Entities
{

    public class LanguageEntity : Entity
    {


        public LanguageEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> languages = Database.getInstance().loadNodeType(entity);

            foreach (long languageId in languages)
            {
                Language dir = new Language(language_id: languageId, films: Database.getInstance().findfilmsByLang(languageId));
                Global.LocalStorage.SaveLanguage(dir);
            }


        }

        public void Add(Language dir)
        {

            string name = dir.name;
            string arr = "INSERT INTO Language (name) VALUES(@Name)";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                dir.language_id = Database.getInstance().getLast();

                Global.LocalStorage.SaveLanguage(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public Language getLanguageByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var language in Global.LocalStorage.Language_Accessor_Selector())
            {
                if (language.language_id == ID)
                    return language;

            }
            return null;
        }
        public void RemoveLanguage(int ID)
        {
            var language = getLanguageByID(ID);

            string arr = "DELETE FROM Language WHERE language_id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(language.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
