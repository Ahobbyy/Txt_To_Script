using System.Collections.Generic;
using System;
using UnityEngine;

namespace Tool.Database
{
    public class InfoData
    {
        /// <summary>
		///姓名
		/// </summary>
		public string Name;
		/// <summary>
		///年龄
		/// </summary>
		public int Age;
		/// <summary>
		///职业
		/// </summary>
		public string Profession;
    }

    public class InfoDatabase : IDatabase
    {
        public const uint TYPE_ID =1;
        public const string DATA_PATH ="Config/Info";

        private List<InfoData> m_datas;

        public  InfoDatabase() { }

        public uint TypeID()
        {
            return TYPE_ID;
        }

        public string DataPath()
        {
            return DATA_PATH;
        }

        public void Load()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(DataPath());
            m_datas = GetAllData(CSVConverter.SerializeCSVData(textAsset));
        }

		private List<InfoData> GetAllData(string[][] m_datas)
		{
			List<InfoData> m_tempList = new List<InfoData>();
			for (int i = 0; i < m_datas.Length; i++)
            {
				InfoData m_tempData = new InfoData();
                m_tempData.Name=m_datas[i][0];
					
				if (!int.TryParse(m_datas[i][1],out m_tempData.Age))
				{
					m_tempData.Age=0;
				}

					m_tempData.Profession=m_datas[i][2];
				m_tempList.Add(m_tempData);
            }
            return m_tempList;
		}

        public InfoData GetDataByKey(string key)
        {
			return m_datas.Find(temp => temp.Name == key);
        }

		public List<InfoData> FindAll(Predicate<InfoData> handler = null)
		{
			if (handler == null)
            {
                return m_datas;
            }
            else
            {
                return m_datas.FindAll(handler);
            }
		}

        public int GetCount()
        {
			return m_datas.Count;
        }
    }
}

