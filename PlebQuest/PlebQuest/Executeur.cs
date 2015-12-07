using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


public class CExecuteur
{
    //string de connexion par default
    private const string DEFAULT_STRING = "Server=J-C222-OL-16;Database=PlebQuest;User Id=1253250;Password=950308";

    //connexion utilise
    private SqlConnection conSql;

    //données de commandes, disponibles par accesseurs
    private SqlCommand cmdSql;
    private SqlDataReader reader;

    public enum Genre { NomTable, NomPS }

    public CExecuteur()
    {
        conSql = new SqlConnection();
        conSql.ConnectionString = DEFAULT_STRING;
    }

    /// <summary>
    /// modification de la bd à l'intérieur de la chaine de connexion présentement active.
    /// </summary>
    /// <param name="nomBD"></param>
    public void ChangerBD(string nomBD)
    {
        conSql.ConnectionString = conSql.ConnectionString.Replace(conSql.Database, nomBD);
    }

    /// <summary>
    /// modification du serveur à l'intérieur de la chainede connexion présentement active.
    /// </summary>
    /// <param name="nomServeur"></param>
    public void ChangerServer(string nomServeur)
    {
        conSql.ConnectionString = conSql.ConnectionString.Replace(conSql.DataSource, nomServeur);
    }

    /// <summary>
    /// Déterminer si la connexion est valide. Pour le savoir il faut tenter une ouverture.
    /// </summary>
    /// <returns></returns>
    public bool ConnexionValide()
    {
        try
        {
            conSql.Open();
            conSql.Close();
        }
        catch
        {
            return false;
        }

        return true;
    }

   /* /// <summary>
    /// obtenir la structure d'une instruction sans recuperer des exceptions
    /// </summary>
    /// <param name="inst"></param>
    /// <param name="nomTable"></param>
    /// <returns></returns>
    public DataTable ObtenirStruct(string inst, string nomTable)
    {
        cmdSql = new SqlCommand(inst, conSql);
        cmdSql.CommandType = CommandType.Text;

        conSql.Open();

        reader = cmdSql.ExecuteReader(CommandBehavior.SchemaOnly);

        DataTable tbStruct = reader.GetSchemaTable();
        tbStruct.TableName = nomTable;

        reader.Close();
        conSql.Close();

        return tbStruct;
    }

    /// <summary>
    /// obtention d'une table (DataTable) contenant la structure d'une instruction SQL, du format SELECT * FROM Table/Vue, ou encore EXECUTE psQuelconque
    /// </summary>
    /// <param name="inst"></param>
    /// <returns></returns>
    public DataTable ObtenirStructure(string inst)
    {
        cmdSql = new SqlCommand(inst, conSql);
        cmdSql.CommandType = CommandType.Text;

        //il faut s'assurer de faire certaines déclarations à l'extérieur d'une instruction Try..Catch...

        conSql.Open();
        DataTable tbStruct;

        try
        {
            //executer la commande
            reader = cmdSql.ExecuteReader(CommandBehavior.SchemaOnly);
            //recuperer le schema
            tbStruct = reader.GetSchemaTable();

            reader.Close();
        }
        catch (Exception e)
        {
            tbStruct = null;
            MessageBox.Show("erreur ObtenirStructure");
        }
        finally
        {
            conSql.Close();
        }

        return tbStruct;
    }

    //
    public DataTable ObtenirStructure(string inst, string nomTable)
    {
        DataTable tbStructure;

        tbStructure = ObtenirStructure(inst);

        if ((tbStructure != null))
            tbStructure.TableName = nomTable;

        return tbStructure;
    }

    public DataTable ObtenirStructure(string nom, Genre genre)
    {
        string inst;
        inst = Convert.ToString((genre == Genre.NomTable ? "SELECT * FROM " + nom : "exec " + nom));

        DataTable tbStructure = ObtenirStruct(inst, nom);

        return tbStructure;
    }

    public int ExtraireLigne(string inst, DataTable tableCible)
    {
        SqlCommand cmd = new SqlCommand(inst, conSql);
        cmd.CommandType = CommandType.Text;

        int i = 0;

        try
        {
            conSql.Open();

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                i++;
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                tableCible.Rows.Add(values);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            tableCible = null;
        }
        finally
        {
            conSql.Close();
        }

        return i;
    }

    
    //public DataTable ExtraireLigne(string inst)
    //{
    //    DataTable tabResult = new DataTable();
    //
    //    DataTable tabStruct = ObtenirStruct(inst, "tempo");
    //
    //    for (int i = 0; i < tabStruct.Rows.Count; i++)
    //    {
    //        DataColumn col = new DataColumn(Convert.ToString(tabStruct.Rows[i]["ColumnName"], Convert.ToString(tabStruct.Rows[i]["DataType"]), System.Type));
    //        //UneColonne = new DataColumn(Convert.ToString(tabStructure.Rows[indice].Item("ColumnName")), (System.Type)tabStructure.Rows[indice].Item("DataType"));
    //
    //        tabResult.Columns.Add(col);
    //    }
    //
    //    ExtraireLigne(inst, tabResult);
    //    return tabResult;
    //}

    public DataTable ExtraireTable(string inst, DataTable tabstruct)
    {
        DataTable tabTemp = TransposerCol(tabstruct);
        ExtraireLigne(inst, tabTemp);
        return tabTemp;

    }

    public DataTable ExtraireTable(string name, Genre genre, DataTable tabstruct)
    {
        string inst = Convert.ToString((genre == Genre.NomTable ? "SELECT * FROM " + name : "EXECUTE " + name));

        return ExtraireTable(inst, tabstruct);
    }

    public DataTable TransposerCol(DataTable tabSource)
    {
        DataTable tabResult = new DataTable();

        foreach (DataRow line in tabSource.Rows)
        {
            DataColumn col = new DataColumn();

            col.ColumnName = Convert.ToString(line["ColomnName"]);
            col.DataType = line["DataType"].GetType();

            tabResult.Columns.Add(col);
        }

        return tabResult;
    }

    public int ExecProcParams(string inst, string code)
    {
        cmdSql = new SqlCommand(inst, conSql);
        cmdSql.CommandType = CommandType.StoredProcedure;

        cmdSql.Parameters.AddWithValue("@code", code);

        int i = 0;
        try
        {
            i = cmdSql.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("ExecProcParams error: " + ex.Message);
        }
        finally
        {
            conSql.Close();
        }

        return i;
    }
    */

    /// <summary>
    /// Appel une procedure stocké
    /// </summary>
    /// <param name="namePs">nom de la procedure</param>
    /// <param name="tabTarget">DataTable dans la quel on vx le resultat</param>
    /// <param name="param">parametre de la SP</param>
    /// <returns>le resultat d'une procedure stocké</returns>
    public bool ExecPsParams(string namePs, DataTable tabTarget, params object[] param)
    {
        string[] nameParam = DonnerNomsParams(namePs);
        bool result = false;

        if (nameParam != null)
        {
            cmdSql = new SqlCommand(namePs, conSql);
            cmdSql.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < nameParam.Length; i++)
            {
                cmdSql.Parameters.AddWithValue(nameParam[i], param[i]);
            }

            try
            {
                conSql.Open();

                reader = cmdSql.ExecuteReader();

                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    tabTarget.Rows.Add(values);
                }

                reader.Close();
                result = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error ExecPsParams: " + ex.Message);
                result = false;
            }
            finally
            {
                conSql.Close();
            }
        }
        return result;
    }
    /*
    public string ExecProcAction(string inst, int code, int achat)
    {
        cmdSql = new SqlCommand(inst, conSql);
        cmdSql.CommandType = CommandType.StoredProcedure;

        cmdSql.Parameters.Add("@message", SqlDbType.NVarChar, 500);
        cmdSql.Parameters.AddWithValue("@idperso", code);
        cmdSql.Parameters.AddWithValue("achat", achat);
        cmdSql.Parameters["@message"].Direction = ParameterDirection.Output;
        conSql.Open();

        string result = "";

        try
        {
            cmdSql.ExecuteNonQuery();

            result = cmdSql.Parameters["@message"].Value.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show("ExecProcAction: " + ex.Message);
        }
        finally
        {
            conSql.Close();
        }

        return result;
    }
    */

    /// <summary>
    /// cherche le nom dun parametre
    /// </summary>
    /// <param name="namePs">nom de la procedure stocké</param>
    /// <returns>le nom du parametre</returns>
    public string[] DonnerNomsParams(string namePs)
    {
        string[] nameParams = { };
        int i = 0;
        string inst = "SELECT PARAMETER_NAME, DATA_TYPE, ORDINAL_POSITION " + "FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME ='" + namePs + "'";
        SqlCommand cmd = new SqlCommand(inst, conSql);
        cmd.CommandType = CommandType.Text;

        try
        {
            conSql.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Array.Resize(ref nameParams, i + 1);
                nameParams[i] = Convert.ToString(reader.GetValue(0));
                i += 1;
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("error DonnerNomsParams: " + ex.Message);
            nameParams = null;
        }
        finally
        {
            conSql.Close();
        }
        return nameParams;
    }

    /// <summary>
    /// Appel une view
    /// </summary>
    /// <param name="NomView">nom de la view a appeler</param>
    /// <param name="targetTable">la DataTable a remplir avec le resultat de la view</param>
    /// <returns>view donne en parametre</returns>
    public bool ExecView(string NomView, DataTable targetTable)
    {
        bool result = false;

        cmdSql = conSql.CreateCommand();
        cmdSql.CommandText = "SELECT * FROM " + NomView;

        try
        {
            conSql.Open();
            reader = cmdSql.ExecuteReader();

            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                targetTable.Rows.Add(values);
            }
            
            reader.Close();
            result = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("error ExecView: " + ex.Message);
            result = false;
        }
        finally
        {
            conSql.Close();
        }

        return result;
    }
}


