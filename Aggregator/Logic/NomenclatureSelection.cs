/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 26.03.2017
 * Время: 18:13
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using Aggregator.Data;

namespace Aggregator.Logic
{
	/// <summary>
	/// Description of NomenclatureSelection.
	/// </summary>
	/// 
	
	public struct Price {
			public String counteragentName;
			public String priceName;
	}
	
	public struct Nomenclature {
			public String nomenclatureName;
			
	}
	
	public class NomenclatureSelection
	{
		List<Price> priceList;
		List<Nomenclature> nomenclatureList;
		
		OleDbConnection oleDbConnection;
		OleDbCommand oleDbCommand;
		OleDbDataReader oleDbDataReader;
		
		SqlDataReader sqlDataReader;
		SqlCommand sqlCommand;
		SqlConnection sqlConnection;
		
		public NomenclatureSelection()
		{
			priceList = new List<Price>();
			nomenclatureList = new List<Nomenclature>();
		}
		
		public void setPrices(ListView listViewPrices)
		{
			priceList = new List<Price>();
			int count = listViewPrices.Items.Count;
			Price price;
			for(int i = 0; i < count; i++)
			{
				price.counteragentName = listViewPrices.Items[i].SubItems[1].Text;
				price.priceName = listViewPrices.Items[i].SubItems[2].Text;
				priceList.Add(price);
			}
		}
		
		public List<Nomenclature> getFindNomenclature(String nomenclatureID)
		{
			nomenclatureList = new List<Nomenclature>();
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDbConnection = new OleDbConnection();
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.localDatabase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
				oleDbConnection.Open();
				
				Nomenclature templeteNomenclature;
				oleDbCommand = new OleDbCommand("SELECT * FROM Nomenclature WHERE (id = " + nomenclatureID + ")", oleDbConnection);
				oleDbDataReader = oleDbCommand.ExecuteReader();
				oleDbDataReader.Read();
				templeteNomenclature.nomenclatureName = oleDbDataReader["name"].ToString();
				oleDbDataReader.Close();
				
				/*
				foreach(Price price in priceList){
					oleDbCommand = new OleDbCommand("SELECT * FROM " + price.priceName, oleDbConnection);
					oleDbDataReader = oleDbCommand.ExecuteReader();
					Nomenclature nomenclature;
			        while (oleDbDataReader.Read())
			        {
			        	nomenclature.nomenclatureName = oleDbDataReader[0].ToString();
			        	nomenclatureList.Add(nomenclature);
			        }
			        oleDbDataReader.Close();
				}
				*/
				oleDbConnection.Close();
				
				return nomenclatureList;
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				
				
				return nomenclatureList;
			}
			
			return null;
		}
		
		
	}
}
