Imports System.Data.SqlClient

Public Class CExecuteur
    'Chaine de connexion par défaut, modifiée selon le TP
    Private Const CHAINE_DEFAUT As String = "Server=J-C236-OL-11;Database=PQSGJPL;User Id=BD2SGJPL;Password=PQ123;"

    ' Connexion utilisée
    Private m_conSQL As SqlConnection

    ' Données de commandes, disponibles par accesseurs
    Private m_cmdSQL As SqlCommand
    Private m_lecteur As SqlDataReader

    Public Enum GenreChaine
        NomTable
        NomPS
    End Enum

    ' Constructeurs
    ' Constructeur par défaut (Avec la chaîne de connexion par défaut)
    Public Sub New()
        MyBase.New()

        m_conSQL = New SqlConnection
        m_conSQL.ConnectionString = CHAINE_DEFAUT

    End Sub


#Region "Les méthodes"


#Region "Modifications de la bd"



    ' modification de la bd à l'intérieur de la chaine
    ' de connexion présentement active.
    Public Sub ChangerBD(ByVal nomBD As String)
        ' on veut changer, dans la chaine de connexion, le nom
        ' de la bd à utiliser.

        ' ici on ne peut pas utiliser SqlConnection.ChangeDatabase
        ' qui exige que la connexion soit présentement ouverte.
        ' m_conSQL.ChangeDatabase(nomBD)

        m_conSQL.ConnectionString = m_conSQL.ConnectionString.Replace(m_conSQL.Database, nomBD)

    End Sub

    '----------------------------------------------------------

    ' modification du serveur à l'intérieur de la chaine
    ' de connexion présentement active.
    ' Remarque: tel qu'écrit il ne faudrait pas que la chaine de
    '           connexion ne contienne plus d'une fois le nom de
    '           serveur car cela causerait des problèmes....

    Public Sub ChangerServeur(ByVal nomServeur As String)
        ' on veut changer, dans la chaine de connexion, le nom
        ' de la bd à utiliser.

        m_conSQL.ConnectionString = m_conSQL.ConnectionString.Replace(m_conSQL.DataSource, nomServeur)

    End Sub

    '----------------------------------------------------------

#End Region


#Region "Validation de la connexion"

    ' Déterminer si la connexion est valide. Pour le savoir
    ' il faut tenter une ouverture.
    Public Function ConnexionValide() As Boolean

        Dim retour As Boolean = True

        Try
            Me.m_conSQL.Open()
            Me.m_conSQL.Close()

        Catch ex As Exception
            retour = False
        End Try

        Return retour

    End Function
#End Region

    '----------------------------------------------------------

#Region "Sur les structures de tables, vues ou ps"


    ' obtention de la structure d'une instruction
    ' SANS récupération des exceptions
    Public Function ObtenirStruct(ByVal inst As String, _
                    ByVal NomTable As String) As DataTable

        Dim cmdSQL As New SqlCommand(inst, m_conSQL)
        cmdSQL.CommandType = CommandType.Text

        Me.m_conSQL.Open()

        Dim lecteur As SqlDataReader = _
                cmdSQL.ExecuteReader(CommandBehavior.SchemaOnly)

        Dim tbStructure As DataTable = lecteur.GetSchemaTable
        tbStructure.TableName = NomTable

        lecteur.Close()
        Me.m_conSQL.Close()

        Return tbStructure

    End Function

    '----------------------------------------------------------

#End Region


#Region "Surcharges de ObtenirStructure"

    ' obtention d'une table (DataTable) contenant la structure 
    ' d'une instruction SQL, du format SELECT * FROM Table/Vue,
    ' ou encore EXECUTE psQuelconque

    Public Overloads Function ObtenirStructure(ByVal inst As String) _
                                                  As DataTable

        Dim cmdSQL As New SqlCommand(inst, m_conSQL)
        cmdSQL.CommandType = CommandType.Text

        ' il faut s'assurer de faire certaines déclarations à l'exté-
        ' rieur d'une instruction Try..Catch...

        Dim lecteur As SqlDataReader

        Dim tbStructure As DataTable

        Try

            Me.m_conSQL.Open()

            ' exécution de la commande
            lecteur = cmdSQL.ExecuteReader(CommandBehavior.SchemaOnly)

            ' récupération de son schéma
            tbStructure = lecteur.GetSchemaTable

            ' REMARQUE : dans le cas présent, le nom "SchemaTable" sera donné
            '            par défaut à la table.

            lecteur.Close()

        Catch e As Exception

            tbStructure = Nothing
            'MessageBox.Show("erreur " & e.Message)
        Finally
            ' si une erreur survient sur le .ExecuteReader la connexion
            ' n'est pas nécessairememnt fermée. Si elle l'est cela 
            ' ne cause pas de problèmes.
            Me.m_conSQL.Close()

        End Try

        Return tbStructure

    End Function


    '----------------------------------------------------------

    ' obtention d'une table (DataTable) contenant la structure 
    ' d'une instruction SQL, sous les mêmes formats que la surcharge précédente,
    ' en lui affectant comme nom (de table) celui reçu en paramètre

    Public Overloads Function ObtenirStructure(ByVal inst As String, _
                      ByVal NomTable As String) As DataTable


        Dim tbStructure As DataTable

        tbStructure = Me.ObtenirStructure(inst)

        If Not IsNothing(tbStructure) Then
            tbStructure.TableName = NomTable
        End If

        Return tbStructure

    End Function

    '----------------------------------------------------------

    ' Une surcharge de la fonction précédente...
    ' Ici, le paramètre "Nom" est soit le nom d'une table ou vue,
    ' soit le nom d'une procédure stockée.

    Public Overloads Function ObtenirStructure(ByVal Nom As String, _
                        ByVal genre As GenreChaine) As DataTable

        Dim instSQL As String
        instSQL = CType(IIf(genre = GenreChaine.NomTable, "SELECT * FROM " & Nom, "exec " & Nom), String)


        ' ré-utilisons notre autre version....
        Dim tbStructure As DataTable

        tbStructure = Me.ObtenirStructure(instSQL, Nom)

        Return tbStructure

    End Function

    '----------------------------------------------------------

#End Region

    Public Function ExtraireLignes(ByVal instSQL As String, _
                    ByVal TableCible As DataTable) As Integer

        ' pour l'instruction SQL fournie, on extrait les lignes
        ' de données, en générant les lignes correspondantes dans
        ' la table reçue en paramètre.
        ' Il est entendu que la structure de la table est connue, c'est-à-
        ' dire que sa collection Columns existe et est conforme à l'instruction
        ' SQL fournie.

        ' et la fonction retourne le nombre de lignes lues

        Dim LaCmd As New SqlCommand(instSQL, m_conSQL)
        LaCmd.CommandType = CommandType.Text

        Dim Compteur As Integer = 0

        Try


            m_conSQL.Open()

            Dim lecteur As SqlDataReader = LaCmd.ExecuteReader()


            ' placer les données dans la liste
            Do While lecteur.Read()
                Compteur += 1
                Dim valeurs(lecteur.FieldCount - 1) As Object
                lecteur.GetValues(valeurs)
                TableCible.Rows.Add(valeurs)
            Loop

            lecteur.Close()

        Catch ex As Exception
            TableCible = Nothing

        Finally
            m_conSQL.Close()

        End Try

        Return Compteur

    End Function

    '----------------------------------------------------------


    Public Function ExtraireLignes(ByVal instSQL As String) As DataTable
        ' pour l'instruction SQL fournie, on extrait les lignes
        ' de données, en générant une table les contenant

        ' et la fonction retourne cette table

        Dim tabRetour As DataTable
        tabRetour = New DataTable

        ' la structure de la table à retourner dépendra du contenu
        ' de l'instruction ( SQL ) reçue.

        Dim tabStructure As DataTable = Me.ObtenirStruct(instSQL, "tempo")

        ' transférons la structure dans la table à produire ici
        For indice As Integer = 0 To tabStructure.Rows.Count - 1
            Dim UneColonne As DataColumn
            UneColonne = New DataColumn(CType(tabStructure.Rows(indice).Item("ColumnName"), System.String), _
                      CType(tabStructure.Rows(indice).Item("DataType"), System.Type))
            tabRetour.Columns.Add(UneColonne)
        Next

        Me.ExtraireLignes(instSQL, tabRetour)

        Return tabRetour

    End Function


    '----------------------------------------------------------

    Public Overloads Function ExtraireTable(ByVal instSQL As String, _
                                  ByVal tbStructure As DataTable) As DataTable

        ' La fonction reçoit une table contenant la structure de ce qui sera
        ' la table à retourner, dans laquelle se trouveront les données.
        Dim tabTempo As DataTable

        tabTempo = Me.TransposerColonnes(tbStructure)

        Me.ExtraireLignes(instSQL, tabTempo)

        Return tabTempo
    End Function

    Public Overloads Function ExtraireTable(ByVal Nom As String, _
                                    ByVal genre As GenreChaine, _
                                    ByVal tbStructure As DataTable) As DataTable

        Dim instruction As String

        instruction = CType(IIf(genre = GenreChaine.NomTable, "SELECT * FROM " & Nom, _
                                    "EXECUTE " & Nom), System.String)

        Return Me.ExtraireTable(instruction, tbStructure)

    End Function


    '----------------------------------------------------------

    Public Function TransposerColonnes(ByVal tbSource As DataTable) As DataTable

        ' la table source (tbSource) contient le schéma d'une instruction SQL
        ' la fonction retourne une référence sur une table dont
        ' les colonnes seront identifiées à partir de valeurs dans les
        ' lignes de la table source.

        Dim ligne As DataRow

        Dim NouvelleTable As New DataTable

        For Each ligne In tbSource.Rows
            ' de chaque ligne de tbSource, on extrait seulement  "champs"
            ' qui sont suffisant pour définir une DataColumn

            Dim UneColonne As New DataColumn

            UneColonne.ColumnName = CType(ligne("ColumnName"), String)
            UneColonne.DataType = ligne("DataType").GetType

            NouvelleTable.Columns.Add(UneColonne)

        Next

        Return NouvelleTable

    End Function


    '----------------------------------------------------------

    Public Function ExecProcParams(ByVal instSQL As String, ByVal Code As String) As Integer
        ' pour l'exécution d'une procédure stokée avec paramètres
        ' mais qui ne retourne qu'une valeur
        Dim cmdSQL As New SqlCommand(instSQL, Me.m_conSQL)
        cmdSQL.CommandType = CommandType.StoredProcedure

        cmdSQL.Parameters.AddWithValue("@Code", Code)

        Me.m_conSQL.Open()

        ' on ne peut pas déclarer nombre à l'intérieur du TRY ici
        Dim nombre As Integer
        Try
            nombre = cmdSQL.ExecuteNonQuery
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            Me.m_conSQL.Close()
        End Try

        Return nombre


    End Function


    '----------------------------------------------------------

    Public Function ExecPsParams(ByVal NomPs As String, _
                                 ByVal TableCible As DataTable, _
                                 ByVal ParamArray paramètres() As Object) As Boolean

        ' pour l'exécution d'une procédure stockée avec paramètres
        ' qui retourne un ensemble de résultats et qui seront stockés dans 
        ' la DataTable de reour ici.
        ' Le nom de la procédure stockée, ainsi que les valeurs à accorder aux
        ' paramètres de celle-ci sont fournis à la méthode.
        ' L'autre paramètre ici est la DataTable (TableCible) qui connaît la structure
        ' des résultats que la ps devrait donner.
        ' TOUS LES PARAMÈTRES SONT DES PARAMÈTRES D'IMPORTATION.

        ' Dans un premier temps, on interroge le serveur pour obtenir les noms "réels"
        ' des paramètres d'importation qu'attend la ps.

        Dim NomsParams() As String
        Dim Indice As Integer
        Dim retour As Boolean = False

        NomsParams = Me.DonnerNomsParams(NomPs)

        If Not NomsParams Is Nothing Then
            Dim cmdSQL As New SqlCommand(NomPs, Me.m_conSQL)
            cmdSQL.CommandType = CommandType.StoredProcedure

            ' on associe les valeurs des paramètres avec les paramètres en question
            For Indice = 0 To NomsParams.Length - 1
                cmdSQL.Parameters.AddWithValue(NomsParams(Indice), paramètres(Indice))
            Next

            Try
                Me.m_conSQL.Open()
                ' 
                Dim lecteur As SqlDataReader = cmdSQL.ExecuteReader()
                ' placer les données dans la table
                Do While lecteur.Read()
                    Dim valeurs(lecteur.FieldCount - 1) As Object
                    lecteur.GetValues(valeurs)
                    TableCible.Rows.Add(valeurs)
                Loop

                lecteur.Close()
                retour = True

            Catch ex As Exception
                MessageBox.Show("Dans ExecPsParams : " & ex.Message)
                retour = False
            Finally
                Me.m_conSQL.Close()
            End Try
        End If

        Return retour


    End Function

    '----------------------------------------------------------

    Public Function ExecProcAction(ByVal instSQL As String, ByVal Code As Integer, ByVal Achat As Integer) As String
        ' pour l'exécution d'une procédure stokée avec paramètres
        ' mais qui ne retourne qu'une valeur
        Dim cmdSQL As New SqlCommand(instSQL, Me.m_conSQL)
        cmdSQL.CommandType = CommandType.StoredProcedure

        'Entrer les paramètres
        cmdSQL.Parameters.Add("@message", SqlDbType.NVarChar, 500)
        cmdSQL.Parameters.AddWithValue("@idperso", Code)
        cmdSQL.Parameters.AddWithValue("@achat", Achat)
        cmdSQL.Parameters("@message").Direction = ParameterDirection.Output
        Me.m_conSQL.Open()

        Dim msgRetour As String
        msgRetour = ""
        Try
            cmdSQL.ExecuteNonQuery()

            msgRetour = cmdSQL.Parameters("@message").Value
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            Me.m_conSQL.Close()
        End Try

        Return msgRetour


    End Function
#End Region

#Region "Pour ExecuteNonQuery"



    Public Function DonnerNomsParams(ByVal NomPs As String) As String()

        Dim NomsParams() As String = {}

        Dim indice As Integer = 0

        Dim instTSQL As String

        instTSQL = "SELECT PARAMETER_NAME, DATA_TYPE, ORDINAL_POSITION " _
              & "FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME ='" & NomPs & "'"

        Dim LaCmd As New SqlCommand(instTSQL, Me.m_conSQL)
        LaCmd.CommandType = CommandType.Text

        Try
            Me.m_conSQL.Open()

            Dim lecteur As SqlDataReader = LaCmd.ExecuteReader

            Do While lecteur.Read
                ReDim Preserve NomsParams(indice)
                NomsParams(indice) = CType(lecteur.GetValue(0), String)
                indice += 1
            Loop

            lecteur.Close()

        Catch ex As Exception
            MessageBox.Show("erreur SQL-Server : " & ex.Message)
            NomsParams = Nothing

        Finally
            Me.m_conSQL.Close()
        End Try

        Return NomsParams

    End Function
#End Region

    'To Do : DoAction
End Class
