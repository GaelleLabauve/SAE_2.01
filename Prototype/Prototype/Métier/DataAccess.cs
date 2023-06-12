using Npgsql;
using System;
using System.Data;
using System.Windows;

namespace Prototype.M�tier
{
    public class DataAccess
    {
        public NpgsqlConnection? NpgSQLConnect
        {
            get
            ;
            set
            ;
        }

        /// <summary>
        /// Connexion � la base de donn�es
        /// </summary>
        /// <returns> Retourne un bool�en indiquant si la connexion est ouverte ou ferm�e</returns>
        public bool OpenConnection()
        {
            try
            {
                NpgSQLConnect = new NpgsqlConnection
                {
                    ConnectionString = "Server=postgresql-emericsauthier.alwaysdata.net;port=5433;Database=emericsauthier_sae-2.01;Search Path=public;uid=emericsauthier_user;password=user_04;" // ACCES A BD
                };
                NpgSQLConnect.Open();

                return NpgSQLConnect.State.Equals(System.Data.ConnectionState.Open);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// D�connexion de la base de donn�es
        /// </summary>
        private void CloseConnection()
        {
            if (NpgSQLConnect != null)
                if (NpgSQLConnect.State.Equals(System.Data.ConnectionState.Open))
                {
                    NpgSQLConnect.Close();
                }
        }

        /// <summary>
        /// Acc�s � des donn�es en lecture
        /// </summary>
        /// <returns>Retourne un DataTable contenant les lignes renvoy�es par le SELECT</returns>
        /// <param name="getQuery">Requ�te SELECT de s�lection de donn�es</param>
        public DataTable GetData(string getQuery)
        {
            try
            {

                if (OpenConnection())
                {


                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(getQuery, NpgSQLConnect);
                    /*NpgsqlDataAdapter npgsqlAdapter = new NpgsqlDataAdapter
                    {
                        SelectCommand = npgsqlCommand
                    };*/
                    NpgsqlDataAdapter npgsqlAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                    DataTable dataTable = new DataTable();
                    npgsqlAdapter.Fill(dataTable);
                    CloseConnection();


                    return dataTable;

                }
                else
                {

                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                CloseConnection();
                return null;
            }
        }

        /// <summary>
        /// Permet d'ins�rer, supprimer ou modifier des donn�es
        /// </summary>
        /// <returns>Retourne un entier contenant le nombre de lignes ajout�es/supprim�es/modifi�es</returns>
        /// <param name="setQuery">Requ�te SQL permettant d'ins�rer (INSERT), supprimer (DELETE) ou modifier des donn�es (UPDATE)</param>
        public int SetData(string setQuery)
        {
            try
            {
                if (OpenConnection())
                {
                    NpgsqlCommand sqlCommand = new NpgsqlCommand(setQuery, NpgSQLConnect);
                    int modifiedLines = sqlCommand.ExecuteNonQuery();
                    CloseConnection();
                    return modifiedLines;
                }
                else
                    return 0;
            }
            catch
            {

                CloseConnection();
                return 0;
            }
        }

    }
}