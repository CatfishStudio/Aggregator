/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 24.02.2017
 * Time: 7:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ADOX;
using Aggregator.Data;

namespace Aggregator.Database
{
	/// <summary>
	/// Description of CreateDatabase.
	/// 
	/// CreateDatabase - конструктор класса
	/// createBaseOleDb - создаёт базу данных OleDb
	/// createBaseMsSql - создаёт базу данных MSSQL
	/// 
	/// </summary>
	public class CreateDatabase
	{
		private Catalog ADOXCatalog;
		private String path;
		private String type;
		
		public CreateDatabase(String fileName, String baseType)
		{
			path = fileName;
			type = baseType;
			if(type == DataConstants.TYPE_OLEDB){
				createBaseOleDb();
			}else if(type == DataConstants.TYPE_MSSQL){
				createBaseMsSql();
			}
		}
		
		private void createBaseOleDb()
		{
			ADOXCatalog = new Catalog();
			ADOXCatalog.Create(DataConfig.oledbConnectLineBegin + path + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass);
		}
		
		private void createBaseMsSql()
		{
			// ...
		}
		
	}
}
