﻿/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 06.04.2017
 * Время: 8:26
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using Aggregator.Data;

namespace Aggregator.Database.Constants
{
	/// <summary>
	/// Description of ReadingConstants.
	/// </summary>
	public class ReadingConstants
	{
		OleDbConnection oleDbConnection;
		OleDbCommand oleDbCommand;
		OleDbDataReader oleDbDataReader;
		
		SqlConnection sqlConnection;
		SqlCommand sqlCommand;
		SqlDataReader sqlDataReader;
		
		public ReadingConstants()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDbConnection = new OleDbConnection();
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.localDatabase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				sqlConnection = new SqlConnection(DataConfig.serverConnection);
			}
		}
		
		public void read()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				try{
					oleDbConnection.Open();
					oleDbCommand = new OleDbCommand("SELECT [id], [name], [email], [address], [vat], [units] FROM Constants", oleDbConnection);
					oleDbDataReader = oleDbCommand.ExecuteReader();
					oleDbDataReader.Read();
					DataConstants.ConstFirmName = oleDbDataReader["name"].ToString();
					DataConstants.ConstFirmEmail = oleDbDataReader["email"].ToString();
					DataConstants.ConstFirmAddress = oleDbDataReader["address"].ToString();
					DataConstants.ConstFirmVAT = (Double)oleDbDataReader["vat"];
					DataConstants.ConstFirmUnits = oleDbDataReader["units"].ToString();
					oleDbDataReader.Close();
					oleDbConnection.Close();
				}catch(Exception ex){
					oleDbConnection.Close();
					Utilits.Console.Log("[ОШИБКА] " + ex.Message.ToString(), false, true);
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				try{
					sqlCommand = new SqlCommand("SELECT [id], [name], [email], [address], [vat], [units] FROM Constants", sqlConnection);
					sqlDataReader = sqlCommand.ExecuteReader();
					sqlDataReader.Read();
					DataConstants.ConstFirmName = sqlDataReader["name"].ToString();
					DataConstants.ConstFirmEmail = sqlDataReader["email"].ToString();
					DataConstants.ConstFirmAddress = sqlDataReader["address"].ToString();
					DataConstants.ConstFirmVAT = (Double)sqlDataReader["vat"];
					DataConstants.ConstFirmUnits = oleDbDataReader["units"].ToString();
					sqlDataReader.Close();
					sqlConnection.Close();
				}catch(Exception ex){
					sqlConnection.Close();
					Utilits.Console.Log("[ОШИБКА] " + ex.Message.ToString(), false, true);
				}
			}
		}
		
		public void Dispose()
		{
			if(oleDbConnection != null){
				if(oleDbDataReader != null) oleDbDataReader.Close();
				oleDbConnection.Close();
				oleDbCommand.Dispose();
				oleDbConnection.Dispose();
			}
			if(sqlConnection != null){
				if(sqlDataReader != null) sqlDataReader.Close();
				sqlConnection.Close();
				sqlCommand.Dispose();
				sqlConnection.Dispose();
			}
		}
	}
}
