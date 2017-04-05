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
using System.Timers;
using System.Windows.Forms;
using System.Data.OleDb;
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
			
			String criteriasSearch = getCriteriasSearch(nomenclatureID);
			
			oleDbConnection.Open();
			foreach(Price price in priceList){
				oleDbCommand = new OleDbCommand("SELECT * FROM " + price.priceName + " " + criteriasSearch, oleDbConnection);
				
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
		
		String getCriteriasSearch(String nomenclatureID)
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
			
			
			// Начало построения запроса
			String stringQuery = "WHERE ";
			
			// По Наименованию - точное совпадение
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
			//stringQuery += str;
			if(str != "") {
				stringQuery += str;
				stringQuery += " OR ";
			}
			
			// По Коду, Серии, Артиклу
			stringQuery += "(code = '" + templeteNomenclature.Code + "' AND code <> '')"+
						" OR (series = '" + templeteNomenclature.Series + "' AND series <> '')"+
						" OR (article = '" + templeteNomenclature.Article + "' AND article <> '')";
			
			// По Наименованию - частичное совпадение
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
			if(str != "" && str != "()") stringQuery += " OR " + str;
			
			// Упорядочить
			stringQuery += " ORDER BY price ASC";
			
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
		
		bool ignoreNumber(String str)
		{
			Regex rgx = new Regex(@"[0-9]");
			return rgx.IsMatch(str);
		}
		
		bool ignoreSymbol(String str)
		{
			Regex rgx = new Regex(@"[/,№,\\]");
			return rgx.IsMatch(str);
		}
		
		/* AUTOMATION ======================================================================= */
		public void autoFindNomenclature(ListView sourceListView)
		{
			String criteriasSearch;
			String nomenclatureID;
			int count = sourceListView.Items.Count;
			
			for(int i = 0; i < count; i++) {
				// получаем сформированный запрос по критериям выбранной номенклатуры
				nomenclatureID = sourceListView.Items[i].SubItems[1].Text;
				criteriasSearch = getAutoCriteriasSearch(nomenclatureID);
				
				// обработка прайс листов по выбранной номенклатуре
				foreach(Price price in priceList){
					oleDbConnection.Open();
					oleDbCommand = new OleDbCommand("SELECT * FROM " + price.priceName + " " + criteriasSearch, oleDbConnection);
					oleDbDataReader = oleDbCommand.ExecuteReader();
					oleDbDataReader.Read();
			        
					sourceListView.Items[i].SubItems[6].Text = oleDbDataReader["name"].ToString();
		        	sourceListView.Items[i].SubItems[7].Text = oleDbDataReader["price"].ToString();
		        	sourceListView.Items[i].SubItems[8].Text = oleDbDataReader["manufacturer"].ToString();
		        	sourceListView.Items[i].SubItems[9].Text = oleDbDataReader["remainder"].ToString();
		        	sourceListView.Items[i].SubItems[10].Text = oleDbDataReader["term"].ToString();
		        	sourceListView.Items[i].SubItems[11].Text = oleDbDataReader["discount1"].ToString();
		        	sourceListView.Items[i].SubItems[12].Text = oleDbDataReader["discount2"].ToString();
		        	sourceListView.Items[i].SubItems[13].Text = oleDbDataReader["discount3"].ToString();
		        	sourceListView.Items[i].SubItems[14].Text = oleDbDataReader["discount4"].ToString();
		        	sourceListView.Items[i].SubItems[15].Text = oleDbDataReader["code"].ToString();
		        	sourceListView.Items[i].SubItems[16].Text = oleDbDataReader["series"].ToString();
		        	sourceListView.Items[i].SubItems[17].Text = oleDbDataReader["article"].ToString();
		        	sourceListView.Items[i].SubItems[18].Text = price.counteragentName;
		        	sourceListView.Items[i].SubItems[19].Text = price.priceName;
		        	
			        oleDbDataReader.Close();
			        oleDbConnection.Close();
				}
				
				DataForms.FClient.messageInStatus("Пожалуйста подождите идет процесс обработки списка номенклатуры " + (i+1).ToString() + "/" + count.ToString());
				DataForms.FClient.Update();
				System.Threading.Thread.Sleep(50);
			}
			MessageBox.Show("Обработка списка номенклатуры - завершена!", "Сообщение");
		}
		
		String getAutoCriteriasSearch(String nomenclatureID)
		{
			// получаем данные выбранной номенклатуры
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
			
			// формируем запрос
			String stringQuery = "WHERE ";
			
			String str = "";
			String[] words =  templeteNomenclature.Name.Split();
			int count = words.Length;
			int i = 0;
			for(i = 0; i < count; i++){
				if(i == 0) str += "(";
				if(words[i].Length > 2 && checkIgnore(words[i]) == false){
					if( i > 0) str += " AND ";
					str += "name LIKE '%" + words[i] + "%'";
				}
				if(ignoreNumber(words[i])){
					if( i > 0) str += " AND ";
					str += "name LIKE '%" + words[i] + "%'";
				}
				if(i == (count-1)) str += ")";
			}
			
			str = str.Replace(".", "%").Replace(",", "%");
			if(str != "") {
				stringQuery += str;
				stringQuery += " OR ";
			}
			
			stringQuery += "(code = '" + templeteNomenclature.Code + "' AND code <> '')"+
						" OR (series = '" + templeteNomenclature.Series + "' AND series <> '')"+
						" OR (article = '" + templeteNomenclature.Article + "' AND article <> '')";
			stringQuery += " ORDER BY price ASC";
			
			return stringQuery;
		}
	}
}
