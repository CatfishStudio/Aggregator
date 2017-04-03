/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 30.03.2017
 * Время: 9:14
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using Aggregator.Data;

namespace Aggregator.Database.Local
{
	/// <summary>
	/// Description of SearchNomenclatureOleDb.
	/// </summary>
	
	public struct Price {
			public String counteragentName;		// наименование контрагента
			public String priceName;			// идентификатор прайслиста
	}
	
	public struct Nomenclature {
			public String Name;					// наименование номенклатуры
			public String Code;					// код номенклатуры
			public String Series;				// серия номенклатуры
			public String Article;				// артикул номенклатуры
			public String Manufacturer;			// производитель номенклатуры
			public String Units;				// единицы измерения номенклатуры
			public Double Remainder;			// остаток номенклатуры
			public Double Price;				// цена номенклатуры
			public Double Discount1;			// скидка 1 номенклатуры
			public Double Discount2;			// скидка 2 номенклатуры
			public Double Discount3;			// скидка 3 номенклатуры
			public Double Discount4;			// скидка 4 номенклатуры
			public DateTime Term;				// срок годности номенклатуры
			public String CounteragentName;		// наименование контрагента
			public String CounteragentPrice;	// идентификатор прайслиста
	}
	
	public class SearchNomenclatureOleDb
	{
		List<Price> priceList;
		List<Nomenclature> nomenclatureList;
		
		OleDbConnection oleDbConnection;
		OleDbCommand oleDbCommand;
		OleDbDataReader oleDbDataReader;
		
		public SearchNomenclatureOleDb()
		{
			priceList = new List<Price>();
			nomenclatureList = new List<Nomenclature>();
			oleDbConnection = new OleDbConnection();
			oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.localDatabase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
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
			
			String selectionCriteria = getSelectionCriteria(nomenclatureID);
			
			oleDbConnection.Open();
			foreach(Price price in priceList){
				oleDbCommand = new OleDbCommand("SELECT * FROM " + price.priceName + " " + selectionCriteria, oleDbConnection);
				
				Utilits.Console.Log("[getFindNomenclature] ЗАПРОС: " + oleDbCommand.CommandText);
				
				oleDbDataReader = oleDbCommand.ExecuteReader();
				Nomenclature nomenclature;
		        while (oleDbDataReader.Read())
		        {
		        	nomenclature = new Nomenclature();
		        	nomenclature.Name = oleDbDataReader["name"].ToString();
		        	nomenclature.Code = oleDbDataReader["code"].ToString();
		        	nomenclature.Series = oleDbDataReader["series"].ToString();
		        	nomenclature.Article = oleDbDataReader["article"].ToString();
		        	nomenclature.Manufacturer = oleDbDataReader["manufacturer"].ToString();
		        	//nomenclature.Units = oleDbDataReader["units"].ToString();
		        	nomenclature.Remainder = (Double)oleDbDataReader["remainder"];
		        	nomenclature.Price = (Double)oleDbDataReader["price"];
		        	nomenclature.Discount1 = (Double)oleDbDataReader["discount1"];
		        	nomenclature.Discount2 = (Double)oleDbDataReader["discount2"];
		        	nomenclature.Discount3 = (Double)oleDbDataReader["discount3"];
		        	nomenclature.Discount4 = (Double)oleDbDataReader["discount4"];
		        	nomenclature.Term = (DateTime)oleDbDataReader["term"];
		        	nomenclature.CounteragentName = price.counteragentName;
		        	nomenclature.CounteragentPrice = price.priceName;
		        	nomenclatureList.Add(nomenclature);
		        }
		        oleDbDataReader.Close();
			}

			oleDbConnection.Close();
			
			return nomenclatureList;
		}
		
		String getSelectionCriteria(String nomenclatureID)
		{
			oleDbConnection.Open();
			Nomenclature templeteNomenclature;
			oleDbCommand = new OleDbCommand("SELECT * FROM Nomenclature WHERE (id = " + nomenclatureID + ")", oleDbConnection);
			oleDbDataReader = oleDbCommand.ExecuteReader();
			oleDbDataReader.Read();
			templeteNomenclature.Name = oleDbDataReader["name"].ToString();
			templeteNomenclature.Code = oleDbDataReader["code"].ToString();
			templeteNomenclature.Series = oleDbDataReader["series"].ToString();
			templeteNomenclature.Article = oleDbDataReader["article"].ToString();
			templeteNomenclature.Manufacturer = oleDbDataReader["manufacturer"].ToString();
			templeteNomenclature.Price = (Double)oleDbDataReader["price"];
			templeteNomenclature.Units = oleDbDataReader["units"].ToString();
			oleDbDataReader.Close();
			oleDbConnection.Close();
			
			//String textQuery = "WHERE name LIKE '%" + templeteNomenclature.Name + "%'";
			
			String stringQuery = "WHERE ";
			
			String str = "";
			String[] words =  templeteNomenclature.Name.Split();
			int count = words.Length;
			int i = 0;
			for(i = 0; i < count; i++){
				if(i == 0) str += "(";
				if(words[i].Length > 2){
					if( i > 0) str += " AND ";
					str += "name LIKE '%" + words[i] + "%'";
				}
				if(i == (count-1)) str += ")";
			}
			
			str = str.Replace(".", "").Replace(",", "");
			stringQuery += str;
			
			str = "";
			count = words.Length;
			for(i = 0; i < count; i++){
				if(i == 0) str += "(";
				if(words[i].Length > 3 && checkIgnore(words[i]) == false){
					if( i > 0) str += " OR ";
					str += "name LIKE '%" + words[i] + "%'";
				}
				if(i == (count-1)) str += ")";
			}
			
			str = str.Replace(".", "").Replace(",", "");			
			stringQuery += " OR " + str;
			
			return stringQuery;
		}
		
		bool checkIgnore(String str)
		{
			Regex rgx = new Regex(@"[0-9]"); //@"[a-zA-Z0-9]" 
			if(rgx.IsMatch(str)) return true;
			rgx = new Regex(@"[/,№,\\]");
			if(rgx.IsMatch(str)) return true;
			return false;
		}
	}
}
