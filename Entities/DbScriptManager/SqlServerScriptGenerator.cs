using ARQ.Maqueta.Entities.Properties;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ARQ.Maqueta.Entities
{
    public class SqlServerScriptGenerator : IScriptGenerator
    {
        //#region IScriptGenerator Members

        //public string GenerateObjectsRulesScript(IEnumerable<EntityTableMapping> entityTableMappings)
        //{
        //    string script = string.Empty;

        //    if (0 < entityTableMappings.Count())
        //    {
        //        script = SqlServerScripts.TryScript;

        //        foreach (var entityMapping in entityTableMappings)
        //        {
        //            foreach (var objectRule in entityMapping.ObjectRules)
        //            {
        //                script = GenerateEntityObjectRulesScript(script, entityMapping, objectRule);
        //            }
        //        }

        //        script += SqlServerScripts.CatchScript;
        //    }

        //    return script;
        //}

        //#endregion

        //#region Private Methods

        //private string GenerateEntityObjectRulesScript(string script, EntityTableMapping entityMapping, ObjectRule objectRule)
        //{
        //    switch (objectRule.ObjectRuleType)
        //    {
        //        case ObjectRuleType.Unique:
        //            script += "\n" + string.Format(CultureInfo.InvariantCulture, SqlServerScripts.CheckObjectExistScript, objectRule.Signature, entityMapping.TableName) + "\nBEGIN\n";
        //            script += string.Format(CultureInfo.InvariantCulture, SqlServerScripts.AddUniqueConstriantScript, GetTableSchema(entityMapping.TableSchema), entityMapping.TableName, objectRule.Signature, GetColumnNames(objectRule.Properties));
        //            break;
        //        case ObjectRuleType.Index:
        //            script += "\n" + string.Format(CultureInfo.InvariantCulture, SqlServerScripts.CheckIfIndexExist, objectRule.Signature) + "\nBEGIN\n";
        //            script += string.Format(CultureInfo.InvariantCulture, SqlServerScripts.CreateIndexScript, objectRule.Signature, GetTableSchema(entityMapping.TableSchema), entityMapping.TableName, GetColumnNames(objectRule.Properties));
        //            break;
        //        default:
        //            break;
        //    }

        //    script += "\nEND;";
        //    return script;
        //}

        //private string GetTableSchema(string schema)
        //{
        //    return false == string.IsNullOrEmpty(schema) ? schema : "dbo";
        //}

        //private object GetColumnNames(List<PropertyColumnMapping> properties)
        //{
        //    return properties.Select(P => "[" + P.ColumnName + "]").Aggregate((i, j) => "[" + i + "]" + "," + "[" + j + "]");
        //}

        //#endregion
    }
}