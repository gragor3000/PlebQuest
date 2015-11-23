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

    public enum Genre { NomTable, NomPS}

    public CExecuteur()
    {
        conSql = new SqlConnection();
        conSql.ConnectionString = DEFAULT_STRING;
    }

    //changer de BD
    public void ChangerBD(string nomBD)
    {
        conSql.ConnectionString = conSql.ConnectionString.Replace(conSql.Database, nomBD);
    }

    //changer le serveur de la BD
    public void ChangerServer(string nomServeur)
    {
        conSql.ConnectionString = conSql.ConnectionString.Replace(conSql.DataSource, nomServeur);
    }

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

    //obtenir la structure d'une instruction sans recuperer des exceptions
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

    public DataTable ObtenirStructure(string nom, GenreChaine genre)
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
        }
    }



}

/*
public class CExecuteur
{
    //Chaine de connexion par défaut, modifiée selon le TP

    private const string CHAINE_DEFAUT = "Server=J-C236-OL-11;Database=PQSGJPL;User Id=BD2SGJPL;Password=PQ123;";
    // Connexion utilisée

    private SqlConnection m_conSQL;
    // Données de commandes, disponibles par accesseurs
    private SqlCommand m_cmdSQL;

    private SqlDataReader m_lecteur;
    public enum GenreChaine
    {
        NomTable,
        NomPS
    }

    // Constructeurs
    // Constructeur par défaut (Avec la chaîne de connexion par défaut)
    public CExecuteur() : base()
    {

        m_conSQL = new SqlConnection();
        m_conSQL.ConnectionString = CHAINE_DEFAUT;

    }


    #region "Les méthodes"


    #region "Modifications de la bd"



    // modification de la bd à l'intérieur de la chaine
    // de connexion présentement active.
    public void ChangerBD(string nomBD)
    {
        // on veut changer, dans la chaine de connexion, le nom
        // de la bd à utiliser.

        // ici on ne peut pas utiliser SqlConnection.ChangeDatabase
        // qui exige que la connexion soit présentement ouverte.
        // m_conSQL.ChangeDatabase(nomBD)

        m_conSQL.ConnectionString = m_conSQL.ConnectionString.Replace(m_conSQL.Database, nomBD);

    }

    //----------------------------------------------------------

    // modification du serveur à l'intérieur de la chaine
    // de connexion présentement active.
    // Remarque: tel qu'écrit il ne faudrait pas que la chaine de
    //           connexion ne contienne plus d'une fois le nom de
    //           serveur car cela causerait des problèmes....

    public void ChangerServeur(string nomServeur)
    {
        // on veut changer, dans la chaine de connexion, le nom
        // de la bd à utiliser.

        m_conSQL.ConnectionString = m_conSQL.ConnectionString.Replace(m_conSQL.DataSource, nomServeur);

    }

    //----------------------------------------------------------

    #endregion


    #region "Validation de la connexion"

    // Déterminer si la connexion est valide. Pour le savoir
    // il faut tenter une ouverture.
    public bool ConnexionValide()
    {

        bool retour = true;

        try
        {
            this.m_conSQL.Open();
            this.m_conSQL.Close();

        }
        catch (Exception ex)
        {
            retour = false;
        }

        return retour;

    }
    #endregion

    //----------------------------------------------------------

    #region "Sur les structures de tables, vues ou ps"


    // obtention de la structure d'une instruction
    // SANS récupération des exceptions
    public DataTable ObtenirStruct(string inst, string NomTable)
    {

        SqlCommand cmdSQL = new SqlCommand(inst, m_conSQL);
        cmdSQL.CommandType = CommandType.Text;

        this.m_conSQL.Open();

        SqlDataReader lecteur = cmdSQL.ExecuteReader(CommandBehavior.SchemaOnly);

        DataTable tbStructure = lecteur.GetSchemaTable();
        tbStructure.TableName = NomTable;

        lecteur.Close();
        this.m_conSQL.Close();

        return tbStructure;

    }

    //----------------------------------------------------------

    #endregion


    #region "Surcharges de ObtenirStructure"

    // obtention d'une table (DataTable) contenant la structure 
    // d'une instruction SQL, du format SELECT * FROM Table/Vue,
    // ou encore EXECUTE psQuelconque

    public DataTable ObtenirStructure(string inst)
    {

        SqlCommand cmdSQL = new SqlCommand(inst, m_conSQL);
        cmdSQL.CommandType = CommandType.Text;

        // il faut s'assurer de faire certaines déclarations à l'exté-
        // rieur d'une instruction Try..Catch...

        SqlDataReader lecteur = default(SqlDataReader);

        DataTable tbStructure = default(DataTable);


        try
        {
            this.m_conSQL.Open();

            // exécution de la commande
            lecteur = cmdSQL.ExecuteReader(CommandBehavior.SchemaOnly);

            // récupération de son schéma
            tbStructure = lecteur.GetSchemaTable();

            // REMARQUE : dans le cas présent, le nom "SchemaTable" sera donné
            //            par défaut à la table.

            lecteur.Close();


        }
        catch (Exception e)
        {
            tbStructure = null;
            //MessageBox.Show("erreur " & e.Message)
        }
        finally
        {
            // si une erreur survient sur le .ExecuteReader la connexion
            // n'est pas nécessairememnt fermée. Si elle l'est cela 
            // ne cause pas de problèmes.
            this.m_conSQL.Close();

        }

        return tbStructure;

    }


    //----------------------------------------------------------

    // obtention d'une table (DataTable) contenant la structure 
    // d'une instruction SQL, sous les mêmes formats que la surcharge précédente,
    // en lui affectant comme nom (de table) celui reçu en paramètre

    public DataTable ObtenirStructure(string inst, string NomTable)
    {


        DataTable tbStructure = default(DataTable);

        tbStructure = this.ObtenirStructure(inst);

        if ((tbStructure != null))
        {
            tbStructure.TableName = NomTable;
        }

        return tbStructure;

    }

    //----------------------------------------------------------

    // Une surcharge de la fonction précédente...
    // Ici, le paramètre "Nom" est soit le nom d'une table ou vue,
    // soit le nom d'une procédure stockée.

    public DataTable ObtenirStructure(string Nom, GenreChaine genre)
    {

        string instSQL = null;
        instSQL = Convert.ToString((genre == GenreChaine.NomTable ? "SELECT * FROM " + Nom : "exec " + Nom));


        // ré-utilisons notre autre version....
        DataTable tbStructure = default(DataTable);

        tbStructure = this.ObtenirStructure(instSQL, Nom);

        return tbStructure;

    }

    //----------------------------------------------------------

    #endregion

    public int ExtraireLignes(string instSQL, DataTable TableCible)
    {

        // pour l'instruction SQL fournie, on extrait les lignes
        // de données, en générant les lignes correspondantes dans
        // la table reçue en paramètre.
        // Il est entendu que la structure de la table est connue, c'est-à-
        // dire que sa collection Columns existe et est conforme à l'instruction
        // SQL fournie.

        // et la fonction retourne le nombre de lignes lues

        SqlCommand LaCmd = new SqlCommand(instSQL, m_conSQL);
        LaCmd.CommandType = CommandType.Text;

        int Compteur = 0;


        try
        {

            m_conSQL.Open();

            SqlDataReader lecteur = LaCmd.ExecuteReader();


            // placer les données dans la liste
            while (lecteur.Read())
            {
                Compteur += 1;
                object[] valeurs = new object[lecteur.FieldCount];
                lecteur.GetValues(valeurs);
                TableCible.Rows.Add(valeurs);
            }

            lecteur.Close();

        }
        catch (Exception ex)
        {
            TableCible = null;

        }
        finally
        {
            m_conSQL.Close();

        }

        return Compteur;

    }

    //----------------------------------------------------------


    public DataTable ExtraireLignes(string instSQL)
    {
        // pour l'instruction SQL fournie, on extrait les lignes
        // de données, en générant une table les contenant

        // et la fonction retourne cette table

        DataTable tabRetour = default(DataTable);
        tabRetour = new DataTable();

        // la structure de la table à retourner dépendra du contenu
        // de l'instruction ( SQL ) reçue.

        DataTable tabStructure = this.ObtenirStruct(instSQL, "tempo");

        // transférons la structure dans la table à produire ici
        for (int indice = 0; indice <= tabStructure.Rows.Count - 1; indice++)
        {
            DataColumn UneColonne = default(DataColumn);
            UneColonne = new DataColumn(Convert.ToString(tabStructure.Rows[indice].Item("ColumnName")), (System.Type)tabStructure.Rows[indice].Item("DataType"));
            tabRetour.Columns.Add(UneColonne);
        }

        this.ExtraireLignes(instSQL, tabRetour);

        return tabRetour;

    }


    //----------------------------------------------------------

    public DataTable ExtraireTable(string instSQL, DataTable tbStructure)
    {

        // La fonction reçoit une table contenant la structure de ce qui sera
        // la table à retourner, dans laquelle se trouveront les données.
        DataTable tabTempo = default(DataTable);

        tabTempo = this.TransposerColonnes(tbStructure);

        this.ExtraireLignes(instSQL, tabTempo);

        return tabTempo;
    }

    public DataTable ExtraireTable(string Nom, GenreChaine genre, DataTable tbStructure)
    {

        string instruction = null;

        instruction = Convert.ToString((genre == GenreChaine.NomTable ? "SELECT * FROM " + Nom : "EXECUTE " + Nom));

        return this.ExtraireTable(instruction, tbStructure);

    }


    //----------------------------------------------------------

    public DataTable TransposerColonnes(DataTable tbSource)
    {

        // la table source (tbSource) contient le schéma d'une instruction SQL
        // la fonction retourne une référence sur une table dont
        // les colonnes seront identifiées à partir de valeurs dans les
        // lignes de la table source.

        //DataRow ligne = default(DataRow);

        DataTable NouvelleTable = new DataTable();

        foreach (DataRow ligne in tbSource.Rows)
        {
            // de chaque ligne de tbSource, on extrait seulement  "champs"
            // qui sont suffisant pour définir une DataColumn

            DataColumn UneColonne = new DataColumn();

            UneColonne.ColumnName = Convert.ToString(ligne["ColumnName"]);
            UneColonne.DataType = ligne["DataType"].GetType();

            NouvelleTable.Columns.Add(UneColonne);

        }

        return NouvelleTable;

    }


    //----------------------------------------------------------

    public int ExecProcParams(string instSQL, string Code)
    {
        // pour l'exécution d'une procédure stokée avec paramètres
        // mais qui ne retourne qu'une valeur
        SqlCommand cmdSQL = new SqlCommand(instSQL, this.m_conSQL);
        cmdSQL.CommandType = CommandType.StoredProcedure;

        cmdSQL.Parameters.AddWithValue("@Code", Code);

        this.m_conSQL.Open();

        // on ne peut pas déclarer nombre à l'intérieur du TRY ici
        int nombre = 0;
        try
        {
            nombre = cmdSQL.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);

        }
        finally
        {
            this.m_conSQL.Close();
        }

        return nombre;


    }


    //----------------------------------------------------------

    public bool ExecPsParams(string NomPs, DataTable TableCible, params object[] paramètres)
    {

        // pour l'exécution d'une procédure stockée avec paramètres
        // qui retourne un ensemble de résultats et qui seront stockés dans 
        // la DataTable de reour ici.
        // Le nom de la procédure stockée, ainsi que les valeurs à accorder aux
        // paramètres de celle-ci sont fournis à la méthode.
        // L'autre paramètre ici est la DataTable (TableCible) qui connaît la structure
        // des résultats que la ps devrait donner.
        // TOUS LES PARAMÈTRES SONT DES PARAMÈTRES D'IMPORTATION.

        // Dans un premier temps, on interroge le serveur pour obtenir les noms "réels"
        // des paramètres d'importation qu'attend la ps.

        string[] NomsParams = null;
        int Indice = 0;
        bool retour = false;

        NomsParams = this.DonnerNomsParams(NomPs);

        if ((NomsParams != null))
        {
            SqlCommand cmdSQL = new SqlCommand(NomPs, this.m_conSQL);
            cmdSQL.CommandType = CommandType.StoredProcedure;

            // on associe les valeurs des paramètres avec les paramètres en question
            for (Indice = 0; Indice <= NomsParams.Length - 1; Indice++)
            {
                cmdSQL.Parameters.AddWithValue(NomsParams[Indice], paramètres[Indice]);
            }

            try
            {
                this.m_conSQL.Open();
                // 
                SqlDataReader lecteur = cmdSQL.ExecuteReader();
                // placer les données dans la table
                while (lecteur.Read())
                {
                    object[] valeurs = new object[lecteur.FieldCount];
                    lecteur.GetValues(valeurs);
                    TableCible.Rows.Add(valeurs);
                }

                lecteur.Close();
                retour = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Dans ExecPsParams : " + ex.Message);
                retour = false;
            }
            finally
            {
                this.m_conSQL.Close();
            }
        }

        return retour;


    }

    //----------------------------------------------------------

    public string ExecProcAction(string instSQL, int Code, int Achat)
    {
        // pour l'exécution d'une procédure stokée avec paramètres
        // mais qui ne retourne qu'une valeur
        SqlCommand cmdSQL = new SqlCommand(instSQL, this.m_conSQL);
        cmdSQL.CommandType = CommandType.StoredProcedure;

        //Entrer les paramètres
        cmdSQL.Parameters.Add("@message", SqlDbType.NVarChar, 500);
        cmdSQL.Parameters.AddWithValue("@idperso", Code);
        cmdSQL.Parameters.AddWithValue("@achat", Achat);
        cmdSQL.Parameters["@message"].Direction = ParameterDirection.Output;
        this.m_conSQL.Open();

        string msgRetour = null;
        msgRetour = "";
        try
        {
            cmdSQL.ExecuteNonQuery();

            msgRetour = cmdSQL.Parameters["@message"].Value.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);

        }
        finally
        {
            this.m_conSQL.Close();
        }

        return msgRetour;


    }
    #endregion

    #region "Pour ExecuteNonQuery"



    public string[] DonnerNomsParams(string NomPs)
    {

        string[] NomsParams = {

        };

        int indice = 0;

        string instTSQL = null;

        instTSQL = "SELECT PARAMETER_NAME, DATA_TYPE, ORDINAL_POSITION " + "FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME ='" + NomPs + "'";

        SqlCommand LaCmd = new SqlCommand(instTSQL, this.m_conSQL);
        LaCmd.CommandType = CommandType.Text;

        try
        {
            this.m_conSQL.Open();

            SqlDataReader lecteur = LaCmd.ExecuteReader();

            while (lecteur.Read())
            {
                Array.Resize(ref NomsParams, indice + 1);
                NomsParams[indice] = Convert.ToString(lecteur.GetValue(0));
                indice += 1;
            }

            lecteur.Close();

        }
        catch (Exception ex)
        {
            MessageBox.Show("erreur SQL-Server : " + ex.Message);
            NomsParams = null;

        }
        finally
        {
            this.m_conSQL.Close();
        }

        return NomsParams;

    }
    #endregion

    //To Do : DoAction
}