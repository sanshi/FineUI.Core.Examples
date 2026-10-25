using Newtonsoft.Json.Linq;
using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace FineUI.Core.Examples
{
    public static class DataSourceUtil
    {
        /// <summary>
        /// 加载 XML 文件（示例的菜单、树数据等放在 XML 里）
        /// </summary>
        public static XmlDocument LoadXml(string xmlPath)
        {
            xmlPath = FineUI.Core.PageContext.MapPath(xmlPath);
            string xmlContent = String.Empty;
            using (StreamReader sr = new StreamReader(xmlPath))
            {
                xmlContent = sr.ReadToEnd();
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            return doc;
        }

        #region GetClassDataTable

        /// <summary>
        /// 获取班级的数据表格
        /// </summary>
        /// <returns></returns>
        public static DataTable GetClassDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("LogTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("Desc", typeof(string)));

            DataRow row = table.NewRow();

            row[0] = 101;
            row[1] = "班级一";
            row[2] = 2000;
            row[3] = DateTime.Parse("2000-09-01");
            row[4] = "班级一创建于2000年9月1号，班长胡飞，连续三年获得全校优秀班级称号。";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = "班级二";
            row[2] = 2005;
            row[3] = DateTime.Parse("2005-09-01");
            row[4] = "班级二创建于2005年9月1号，班长董婷婷，连续两年获得全校优秀班级称号。";
            table.Rows.Add(row);

            return table;
        }

        #endregion

        #region GetEmptyDataTable

        /// <summary>
        /// 获取空数据表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmptyDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Code", typeof(String)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("AtSchool", typeof(bool)));
            table.Columns.Add(new DataColumn("Major", typeof(String)));
            table.Columns.Add(new DataColumn("Group", typeof(int)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("LogTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("Desc", typeof(string)));
            table.Columns.Add(new DataColumn("Guid", typeof(Guid)));
            table.Columns.Add(new DataColumn("EntranceDate", typeof(String)));

            return table;
        }

        #endregion

        #region GetDataTable

        private static Random rd = new Random();

        private static string GenerateRandomAvatarUrl(bool isFemale = false)
        {
            var folder = isFemale ? "female" : "male";
            return String.Format("/res/images/avatars/{0}/{1}.jpg", folder, rd.Next(1, 13));
        }

        private static int GenerateRandomStatus()
        {
            return rd.Next(1, 5);
        }

        private static int GenerateRandomRate1()
        {
            return rd.Next(1, 6);
        }

        /// <summary>
        /// 获取模拟表格
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            // 注册日期精确到天；每批只读取一次今天，避免行间带入不同的时间和时区偏移。
            DateTime today = DateTime.Today;
            DateTime baseDate = new DateTime(today.Year, today.Month, today.Day);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("AtSchool", typeof(bool)));
            table.Columns.Add(new DataColumn("Major", typeof(String)));
            table.Columns.Add(new DataColumn("Group", typeof(int)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("LogTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("Desc", typeof(string)));
            table.Columns.Add(new DataColumn("Guid", typeof(Guid)));

            // Hobby：reading,basketball,travel,movie,music
            // 爱好：读书, 篮球, 旅游, 电影, 音乐
            table.Columns.Add(new DataColumn("Hobby", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceDate", typeof(String)));

            // 考试成绩
            table.Columns.Add(new DataColumn("ChineseScore", typeof(Int32)));
            table.Columns.Add(new DataColumn("MathScore", typeof(Int32)));
            table.Columns.Add(new DataColumn("TotalScore", typeof(Int32)));

            // 体检结果
            table.Columns.Add(new DataColumn("ShenGao", typeof(Int32)));
            table.Columns.Add(new DataColumn("TiZhong", typeof(Int32)));
            table.Columns.Add(new DataColumn("XueYaDi", typeof(String)));
            table.Columns.Add(new DataColumn("XueYaGao", typeof(String)));
            table.Columns.Add(new DataColumn("ShiLiZuo", typeof(float)));
            table.Columns.Add(new DataColumn("ShiLiYou", typeof(float)));
            table.Columns.Add(new DataColumn("ShiLiZuoJiaoZhen", typeof(float)));
            table.Columns.Add(new DataColumn("ShiLiYouJiaoZhen", typeof(float)));

            // 合并单元格需要的字段
            table.Columns.Add(new DataColumn("GroupB", typeof(int)));

            // 工资
            table.Columns.Add(new DataColumn("Salary", typeof(float)));

            // 进度
            table.Columns.Add(new DataColumn("Progress1", typeof(int)));
            table.Columns.Add(new DataColumn("Progress2", typeof(int)));

            // 评分
            table.Columns.Add(new DataColumn("Rate1", typeof(int)));
            table.Columns.Add(new DataColumn("Rate2", typeof(int)));
            table.Columns.Add(new DataColumn("Rate3", typeof(float)));

            // 头像
            table.Columns.Add(new DataColumn("Avatar", typeof(String)));

            // 状态（优秀、良好、补考、重修）
            table.Columns.Add(new DataColumn("Status", typeof(int)));

            DataRow row = table.NewRow();
            row[0] = 101;
            row[1] = "张萍萍"; // <script>alert(1)</script>
            row[2] = 2000;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 1;
            row[6] = 0;
            row[7] = baseDate.AddDays(-100); // DBNull.Value;
            row[8] = "张萍萍，女，20岁，出生于中国南方的一个小山村，毕业于中国科学技术大学。毕业后就职于某大型国有企业，任部门经理，连续三年获得企业优秀员工称号。张萍萍凭借出色的领导能力和创新思维，带领团队完成了多个重大项目，赢得了同事和上级的一致认可。她积极参与公益活动，回馈家乡，致力于推动乡村教育发展，成为年轻一代的榜样。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,travel,music";
            row[11] = "2000-09-01";
            row[12] = 80;
            row[13] = 90;
            row[14] = 170;
            row[15] = 180;
            row[16] = 150;
            row[17] = 80;
            row[18] = 120;
            row[19] = 0.2;
            row[20] = 0.5;
            row[21] = 1.0;
            row[22] = 1.2;
            row[23] = 1;
            row[24] = 12000;
            row[25] = 30;
            row[26] = 90;
            row[27] = GenerateRandomRate1();
            row[28] = 5;
            row[29] = 3.5;
            row[30] = GenerateRandomAvatarUrl(true);
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = "陈飞";
            row[2] = 2000;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 1;
            row[6] = 1;
            row[7] = baseDate.AddDays(-100);
            row[8] = "陈飞，男，20岁，出生于中国北方的一个小山村，毕业于南方科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "basketball,travel,movie,reading,music";
            row[11] = "2001-09-01";
            row[12] = 85;
            row[13] = 90;
            row[14] = 175;
            row[15] = 160;
            row[16] = 120;
            row[17] = 70;
            row[18] = 110;
            row[19] = 0.3;
            row[20] = 0.3;
            row[21] = 1.2;
            row[22] = 1.2;
            row[23] = 3;
            row[24] = 22500;
            row[25] = 60;
            row[26] = 50;
            row[27] = GenerateRandomRate1();
            row[28] = 2;
            row[29] = 2.5;
            row[30] = GenerateRandomAvatarUrl();
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 103;
            row[1] = "董婷婷";
            row[2] = 2000;
            row[3] = true;
            row[4] = "化学系";
            row[5] = 2;
            row[6] = 0;
            row[7] = baseDate.AddDays(-100);
            row[8] = "董婷婷，女，18岁，出生于中国海南岛的一个小山村，毕业于中国科学技术大学。董婷婷是在学校认识丈夫刘国的，有一天晚上下自习后，董婷婷发短信给刘国说“做我男朋友吧！”，然后他们就走到了一起。";
            row[9] = Guid.NewGuid();
            row[10] = "basketball,movie,music";
            row[11] = "2008-09-01";
            row[12] = 90;
            row[13] = 90;
            row[14] = 180;
            row[15] = 190;
            row[16] = 130;
            row[17] = 82;
            row[18] = 125;
            row[19] = 0.8;
            row[20] = 0.6;
            row[21] = 1.2;
            row[22] = 1.0;
            row[23] = 2;
            row[24] = 12500;
            row[25] = 65;
            row[26] = 85;
            row[27] = GenerateRandomRate1();
            row[28] = 0;
            row[29] = 1.5;
            row[30] = GenerateRandomAvatarUrl(true);
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 104;
            row[1] = "刘国";
            row[2] = 2020;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 2;
            row[6] = 1;
            row[7] = baseDate.AddDays(-100);
            row[8] = "刘国，男，22岁，出生于中国澳门的一个小山村，毕业于中国科学技术大学。刘国是作为交换生来中科大学习，在校期间认识了妻子董婷婷，虽然是被追到手了，不过在人前却总是说“老婆是我千辛万苦追来的！”。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball,movie";
            row[11] = "2020-09-01";
            row[12] = 95;
            row[13] = 95;
            row[14] = 190;
            row[15] = 170;
            row[16] = 130;
            row[17] = 76;
            row[18] = 112;
            row[19] = 1.0;
            row[20] = 1.0;
            row[21] = 1.0;
            row[22] = 1.0;
            row[23] = 2;
            row[24] = 15800;
            row[25] = 95;
            row[26] = 55;
            row[27] = GenerateRandomRate1();
            row[28] = 1;
            row[29] = 4;
            row[30] = GenerateRandomAvatarUrl();
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 105;
            row[1] = "康颖颖";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 3;
            row[6] = 0;
            row[7] = baseDate.AddDays(-60);
            row[8] = "康颖颖，女，26岁，出生于中国福建的一个小山村，毕业于香港科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,travel,movie,music";
            row[11] = "2008-09-01";
            row[12] = 85;
            row[13] = 95;
            row[14] = 180;
            row[15] = 159;
            row[16] = 90;
            row[17] = 77;
            row[18] = 128;
            row[19] = 0.6;
            row[20] = 0.9;
            row[21] = 1.2;
            row[22] = 1.2;
            row[23] = 3;
            row[24] = 8800;
            row[25] = 30;
            row[26] = 75;
            row[27] = GenerateRandomRate1();
            row[28] = 0;
            row[29] = 0;
            row[30] = GenerateRandomAvatarUrl(true);
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 106;
            row[1] = "彭博";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 3;
            row[6] = 1;
            row[7] = baseDate.AddDays(-50);
            row[8] = "彭博，男，28岁，出生于中国浙江的一个小山村，毕业于电子科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "basketball,travel,music";
            row[11] = "2003-09-01";
            row[12] = 60;
            row[13] = 100;
            row[14] = 160;
            row[15] = 175;
            row[16] = 150;
            row[17] = 88;
            row[18] = 126;
            row[19] = 0.1;
            row[20] = 0.3;
            row[21] = 1.5;
            row[22] = 1.2;
            row[23] = 3;
            row[24] = 9800;
            row[25] = 0;
            row[26] = 0;
            row[27] = GenerateRandomRate1();
            row[28] = 2;
            row[29] = 5;
            row[30] = GenerateRandomAvatarUrl();
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 107;
            row[1] = "黄婷婷";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 3;
            row[6] = 0;
            row[7] = baseDate.AddDays(-50);
            row[8] = "黄婷婷，女，25岁，出生于中国北方的一个小山村，毕业于北京科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "travel,movie,music,reading";
            row[11] = "2000-09-01";
            row[12] = 100;
            row[13] = 80;
            row[14] = 180;
            row[15] = 160;
            row[16] = 80;
            row[17] = 70;
            row[18] = 110;
            row[19] = 0.8;
            row[20] = 0.8;
            row[21] = 1.2;
            row[22] = 1.2;
            row[23] = 3;
            row[24] = 6888.50;
            row[25] = 20;
            row[26] = 80;
            row[27] = GenerateRandomRate1();
            row[28] = 5;
            row[29] = 0;
            row[30] = GenerateRandomAvatarUrl(true);
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 108;
            row[1] = "唐超";
            row[2] = 2020;
            row[3] = false;
            row[4] = "物理系";
            row[5] = 3;
            row[6] = 1;
            row[7] = baseDate.AddDays(-50);
            row[8] = "唐超，男，26岁，出生于中国河南的一个小山村，毕业于中国科学技术大学。作为刘国的同班同学，唐超是班里的尖子生，本科还没毕业就被哈佛大学录取了，现在好像还没毕业呢。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,movie,music,basketball";
            row[11] = "2020-09-01";
            row[12] = 80;
            row[13] = 80;
            row[14] = 160;
            row[15] = 188;
            row[16] = 130;
            row[17] = 82;
            row[18] = 130;
            row[19] = 0.5;
            row[20] = 0.8;
            row[21] = 1.3;
            row[22] = 1.2;
            row[23] = 3;
            row[24] = 25800;
            row[25] = 0;
            row[26] = 60;
            row[27] = GenerateRandomRate1();
            row[28] = 3;
            row[29] = 4;
            row[30] = GenerateRandomAvatarUrl();
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 109;
            row[1] = "杨婷婷";
            row[2] = 2020;
            row[3] = true;
            row[4] = "物理系";
            row[5] = 5;
            row[6] = 0;
            row[7] = baseDate.AddDays(-50);
            row[8] = "杨婷婷，女，25岁，出生于中国广西的一个小山村，毕业于南方科学技术大学。杨婷婷的父母都是中科大的高材生，他们很高兴送女儿上一所前中科大校长创办的大学，而南科大自己颁发的学位文凭也颇受争议。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball,movie";
            row[11] = "2003-09-01";
            row[12] = 90;
            row[13] = 60;
            row[14] = 150;
            row[15] = 166;
            row[16] = 110;
            row[17] = 70;
            row[18] = 116;
            row[19] = 0.8;
            row[20] = 1.0;
            row[21] = 0.8;
            row[22] = 1.0;
            row[23] = 5;
            row[24] = 35000;
            row[25] = 65;
            row[26] = 30;
            row[27] = GenerateRandomRate1();
            row[28] = 0;
            row[29] = 2.5;
            row[30] = GenerateRandomAvatarUrl(true);
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 110;
            row[1] = "徐鹏";
            row[2] = 2020;
            row[3] = false;
            row[4] = "物理系";
            row[5] = 5;
            row[6] = 1;
            row[7] = baseDate.AddDays(-50);
            row[8] = "徐鹏，男，23岁，出生于中国安徽的一个小山村，毕业于国防科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball,travel";
            row[11] = "2020-09-01";
            row[12] = 60;
            row[13] = 90;
            row[14] = 150;
            row[15] = 198;
            row[16] = 145;
            row[17] = 83;
            row[18] = 128;
            row[19] = 1.2;
            row[20] = 1.5;
            row[21] = 1.2;
            row[22] = 1.5;
            row[23] = 5;
            row[24] = 20800;
            row[25] = 100;
            row[26] = 80;
            row[27] = GenerateRandomRate1();
            row[28] = 3;
            row[29] = 3;
            row[30] = GenerateRandomAvatarUrl();
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 111;
            row[1] = "董国";
            row[2] = 2020;
            row[3] = true;
            row[4] = "自动化系";
            row[5] = 5;
            row[6] = 1;
            row[7] = baseDate.AddDays(-5);
            row[8] = "董国，男，22岁，出生于中国台湾的一个小山村，毕业于台湾科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,movie,music,basketball";
            row[11] = "2006-09-01";
            row[12] = 90;
            row[13] = 70;
            row[14] = 160;
            row[15] = 158;
            row[16] = 76;
            row[17] = 69;
            row[18] = 109;
            row[19] = 0.8;
            row[20] = 0.6;
            row[21] = 1.2;
            row[22] = 1.2;
            row[23] = 5;
            row[24] = 12800;
            row[25] = 0;
            row[26] = 0;
            row[27] = GenerateRandomRate1();
            row[28] = 2;
            row[29] = 3.5;
            row[30] = GenerateRandomAvatarUrl();
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 112;
            row[1] = "张三石";
            row[2] = 2012;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 5;
            row[6] = 1;
            row[7] = baseDate.AddDays(-5);
            row[8] = "张三石，男，28岁，出生于中国河南的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,movie,music";
            row[11] = "2000-09-01";
            row[12] = 99;
            row[13] = 98;
            row[14] = 197;
            row[15] = 183;
            row[16] = 125;
            row[17] = 80;
            row[18] = 120;
            row[19] = 0.8;
            row[20] = 0.6;
            row[21] = 1.2;
            row[22] = 1.2;
            row[23] = 5;
            row[24] = 16800.50;
            row[25] = 80;
            row[26] = 75;
            row[27] = GenerateRandomRate1();
            row[28] = 4;
            row[29] = 5;
            row[30] = GenerateRandomAvatarUrl();
            row[31] = GenerateRandomStatus();
            table.Rows.Add(row);

            return table;
        }


        /// <summary>
        /// 获取模拟表格（简单表格）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSimpleDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("AtSchool", typeof(bool)));
            table.Columns.Add(new DataColumn("Major", typeof(String)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("EntranceDate", typeof(String)));

            // DateOnly 和 TimeOnly 是 .NET 6 引入的结构体
            // https://learn.microsoft.com/en-us/dotnet/standard/datetime/how-to-use-dateonly-timeonly
            table.Columns.Add(new DataColumn("DateOnly1", typeof(DateOnly)));
            table.Columns.Add(new DataColumn("TimeOnly1", typeof(TimeOnly)));

            var nowDate = DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            var nowTime = TimeOnly.Parse(DateTime.Now.ToString("HH:mm:ss"));

            DataRow row = table.NewRow();
            row[0] = 101;
            row[1] = "张萍萍";
            row[2] = 2000;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 0;
            row[6] = "2000-09-01";
            row[7] = nowDate;
            row[8] = nowTime;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = "陈飞";
            row[2] = 2000;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 1;
            row[6] = "2001-09-01";
            row[7] = nowDate.AddDays(1);
            row[8] = nowTime.AddHours(1);
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 103;
            row[1] = "董婷婷";
            row[2] = 2000;
            row[3] = true;
            row[4] = "化学系";
            row[5] = 0;
            row[6] = "2008-09-01";
            row[7] = nowDate.AddDays(1);
            row[8] = nowTime.AddHours(1);
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 104;
            row[1] = "刘国";
            row[2] = 2020;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 1;
            row[6] = "2020-09-01";
            row[7] = nowDate.AddDays(2);
            row[8] = nowTime.AddHours(2);
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 105;
            row[1] = "康颖颖";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 0;
            row[6] = "2008-09-01";
            row[7] = nowDate.AddDays(2);
            row[8] = nowTime.AddHours(2);
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 106;
            row[1] = "彭博";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 1;
            row[6] = "2003-09-01";
            row[7] = nowDate.AddDays(3);
            row[8] = nowTime.AddHours(3);
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 107;
            row[1] = "黄婷婷";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 0;
            row[6] = "2000-09-01";
            row[7] = nowDate.AddDays(3);
            row[8] = nowTime.AddHours(3);
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 108;
            row[1] = "唐超";
            row[2] = 2020;
            row[3] = false;
            row[4] = "物理系";
            row[5] = 1;
            row[6] = "2020-09-01";
            row[7] = nowDate.AddDays(4);
            row[8] = nowTime.AddHours(4);
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 109;
            row[1] = "杨婷婷";
            row[2] = 2020;
            row[3] = true;
            row[4] = "物理系";
            row[5] = 0;
            row[6] = "2003-09-01";
            row[7] = nowDate.AddDays(4);
            row[8] = nowTime.AddHours(4);
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 110;
            row[1] = "徐鹏";
            row[2] = 2020;
            row[3] = false;
            row[4] = "物理系";
            row[5] = 1;
            row[6] = "2020-09-01";
            row[7] = nowDate.AddDays(10);
            row[8] = nowTime.AddHours(10);
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 111;
            row[1] = "董国";
            row[2] = 2020;
            row[3] = true;
            row[4] = "自动化系";
            row[5] = 1;
            row[6] = "2006-09-01";
            row[7] = nowDate.AddDays(10);
            row[8] = nowTime.AddHours(10);
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 112;
            row[1] = "张三石";
            row[2] = 2012;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 1;
            row[6] = "2000-09-01";
            row[7] = nowDate.AddDays(10);
            row[8] = nowTime.AddHours(10);
            table.Rows.Add(row);

            return table;
        }

        #endregion

        #region GetDataTable2

        /// <summary>
        /// 获取模拟表格2
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable2()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("AtSchool", typeof(bool)));
            table.Columns.Add(new DataColumn("Major", typeof(String)));
            table.Columns.Add(new DataColumn("Group", typeof(int)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("LogTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("Desc", typeof(string)));
            table.Columns.Add(new DataColumn("Guid", typeof(Guid)));
            table.Columns.Add(new DataColumn("Hobby", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceDate", typeof(String)));
            table.Columns.Add(new DataColumn("ExtraFee", typeof(int)));
            table.Columns.Add(new DataColumn("Fee", typeof(int)));
            // Hobby：reading,basketball,travel,movie,music
            // 爱好：读书, 篮球, 旅游, 电影, 音乐 

            // 考试成绩
            table.Columns.Add(new DataColumn("ChineseScore", typeof(Int32)));
            table.Columns.Add(new DataColumn("MathScore", typeof(Int32)));
            table.Columns.Add(new DataColumn("TotalScore", typeof(Int32)));

            // 体检结果
            table.Columns.Add(new DataColumn("ShenGao", typeof(Int32)));
            table.Columns.Add(new DataColumn("TiZhong", typeof(Int32)));
            table.Columns.Add(new DataColumn("XueYaDi", typeof(String)));
            table.Columns.Add(new DataColumn("XueYaGao", typeof(String)));
            table.Columns.Add(new DataColumn("ShiLiZuo", typeof(float)));
            table.Columns.Add(new DataColumn("ShiLiYou", typeof(float)));
            table.Columns.Add(new DataColumn("ShiLiZuoJiaoZhen", typeof(float)));
            table.Columns.Add(new DataColumn("ShiLiYouJiaoZhen", typeof(float)));

            // GPA
            table.Columns.Add(new DataColumn("GPA", typeof(float)));


            // 固定注册日期使重新绑定得到相同数据，便于对比分页、排序及不同服务端的响应。
            DataRow row = null;

            row = table.NewRow();
            row[0] = 101;
            row[1] = "陈萍萍";
            row[2] = 2000;
            row[3] = true;
            row[4] = "计算机应用技术";
            row[5] = 1;
            row[6] = 0;
            row[7] = new DateTime(2000, 9, 1);
            row[8] = "陈萍萍，女，20岁，出生于中国南方的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball,travel";
            row[11] = "2000-09-01";
            row[12] = 299;
            row[13] = 2990;
            row[14] = 80;
            row[15] = 60;
            row[16] = 160;
			row[17] = 180;
            row[18] = 150;
            row[19] = 80;
            row[20] = 120;
            row[21] = 0.2;
            row[22] = 0.5;
            row[23] = 1.0;
            row[24] = 1.2;
            row[25] = 3.60;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = "胡飞";
            row[2] = 2008;
            row[3] = true;
            row[4] = "信息工程";
            row[5] = 1;
            row[6] = 1;
            row[7] = new DateTime(2008, 9, 1);
            row[8] = "胡飞，男，20岁，出生于中国北方的一个小山村，毕业于南方科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball";
            row[11] = "2008-09-01";
            row[12] = 199;
            row[13] = 1990;
            row[14] = 90;
            row[15] = 80;
            row[16] = 170;
			row[17] = 185;
            row[18] = 155;
            row[19] = 85;
            row[20] = 125;
            row[21] = 0.3;
            row[22] = 0.6;
            row[23] = 1.2;
            row[24] = 1.4;
            row[25] = 2.69;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 103;
            row[1] = "金婷婷";
            row[2] = 2001;
            row[3] = false;
            row[4] = "会计学";
            row[5] = 2;
            row[6] = 0;
            row[7] = new DateTime(2001, 9, 1);
            row[8] = "金婷婷，女，28岁，出生于中国海南岛的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball,music";
            row[11] = "2001-09-01";
            row[12] = 299;
            row[13] = 3990;
            row[14] = 85;
            row[15] = 85;
            row[16] = 170;
			row[17] = 185;
            row[18] = 155;
            row[19] = 85;
            row[20] = 125;
            row[21] = 0.3;
            row[22] = 0.6;
            row[23] = 1.2;
            row[24] = 1.4;
            row[25] = 2.72;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 104;
            row[1] = "潘国";
            row[2] = 2008;
            row[3] = false;
            row[4] = "国际经济与贸易";
            row[5] = 2;
            row[6] = 1;
            row[7] = new DateTime(2008, 9, 1);
            row[8] = "潘国，男，22岁，出生于中国澳门的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,music";
            row[11] = "2008-09-01";
            row[12] = 399;
            row[13] = 3998;
            row[14] = 89;
            row[15] = 80;
            row[16] = 169;
			row[17] = 190;
            row[18] = 165;
            row[19] = 90;
            row[20] = 130;
            row[21] = 1.2;
            row[22] = 1.2;
            row[23] = 1.2;
            row[24] = 1.4;
            row[25] = 2.69;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 105;
            row[1] = "吴颖颖";
            row[2] = 2020;
            row[3] = true;
            row[4] = "市场营销";
            row[5] = 3;
            row[6] = 0;
            row[7] = new DateTime(2020, 9, 1);
            row[8] = "吴颖颖，女，26岁，出生于中国福建的一个小山村，毕业于香港科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,movie,music";
            row[11] = "2020-09-01";
            row[12] = 499;
            row[13] = 4992;
            row[14] = 92;
            row[15] = 85;
            row[16] = 177;
			row[17] = 190;
            row[18] = 165;
            row[19] = 90;
            row[20] = 130;
            row[21] = 1.2;
            row[22] = 1.2;
            row[23] = 1.2;
            row[24] = 1.4;
            row[25] = 2.50;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 106;
            row[1] = "张博";
            row[2] = 2003;
            row[3] = true;
            row[4] = "财务管理";
            row[5] = 3;
            row[6] = 1;
            row[7] = new DateTime(2003, 9, 1);
            row[8] = "张博，男，28岁，出生于中国浙江的一个小山村，毕业于电子科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "movie,music";
            row[11] = "2003-09-01";
            row[12] = 99;
            row[13] = 997;
            row[14] = 88;
            row[15] = 86;
            row[16] = 174;
			row[17] = 190;
            row[18] = 165;
            row[19] = 90;
            row[20] = 130;
            row[21] = 1.2;
            row[22] = 1.2;
            row[23] = 1.2;
            row[24] = 1.4;
            row[25] = 2.13;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 107;
            row[1] = "杨倩倩";
            row[2] = 2000;
            row[3] = false;
            row[4] = "材料物理与化学";
            row[5] = 4;
            row[6] = 0;
            row[7] = new DateTime(2000, 9, 1);
            row[8] = "杨倩倩，女，25岁，出生于中国北方的一个小山村，毕业于北京科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "travel,movie,music";
            row[11] = "2000-09-01";
            row[12] = 399;
            row[13] = 3995;
            row[14] = 65;
            row[15] = 82;
            row[16] = 177;
			row[17] = 160;
            row[18] = 125;
            row[19] = 60;
            row[20] = 110;
            row[21] = 1.5;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.90;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 108;
            row[1] = "董超";
            row[2] = 2020;
            row[3] = false;
            row[4] = "生物医学工程";
            row[5] = 4;
            row[6] = 1;
            row[7] = new DateTime(2020, 9, 1);
            row[8] = "董超，男，26岁，出生于中国河南的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "basketball,movie,music";
            row[11] = "2020-09-01";
            row[12] = 299;
            row[13] = 2996;
            row[14] = 86;
            row[15] = 60;
            row[16] = 184;
			row[17] = 160;
            row[18] = 125;
            row[19] = 60;
            row[20] = 110;
            row[21] = 1.5;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 109;
            row[1] = "张娟娟";
            row[2] = 2003;
            row[3] = true;
            row[4] = "材料物理与化学";
            row[5] = 5;
            row[6] = 0;
            row[7] = new DateTime(2003, 9, 1);
            row[8] = "张娟娟，女，25岁，出生于中国广西的一个小山村，毕业于南方科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,travel,movie,music";
            row[11] = "2003-09-01";
            row[12] = 599;
            row[13] = 5990;
            row[14] = 92;
            row[15] = 66;
            row[16] = 198;
			row[17] = 160;
            row[18] = 125;
            row[19] = 60;
            row[20] = 110;
            row[21] = 1.5;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 110;
            row[1] = "叶鹏";
            row[2] = 2006;
            row[3] = false;
            row[4] = "电子商务";
            row[5] = 5;
            row[6] = 1;
            row[7] = new DateTime(2006, 9, 1);
            row[8] = "叶鹏，男，23岁，出生于中国安徽的一个小山村，毕业于国防科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,movie,music";
            row[11] = "2006-09-01";
            row[12] = 699;
            row[13] = 6990;
            row[14] = 69;
            row[15] = 99;
            row[16] = 168;
			row[17] = 160;
            row[18] = 125;
            row[19] = 60;
            row[20] = 110;
            row[21] = 1.5;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 111;
            row[1] = "李玲玲";
            row[2] = 2020;
            row[3] = true;
            row[4] = "管理学";
            row[5] = 5;
            row[6] = 0;
            row[7] = new DateTime(2020, 9, 1);
            row[8] = "李玲玲，女，22岁，出生于中国台湾的一个小山村，毕业于台湾科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,travel,music";
            row[11] = "2020-09-01";
            row[12] = 399;
            row[13] = 3990;
            row[14] = 88;
            row[15] = 92;
            row[16] = 180;
			row[17] = 175;
            row[18] = 135;
            row[19] = 80;
            row[20] = 130;
            row[21] = 1.0;
            row[22] = 1.0;
            row[23] = 1.2;
            row[24] = 1.2;
            row[25] = 2.69;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 112;
            row[1] = "张萍萍";
            row[2] = 2000;
            row[3] = true;
            row[4] = "计算机应用技术";
            row[5] = 1;
            row[6] = 0;
            row[7] = new DateTime(2000, 9, 1);
            row[8] = "张萍萍，女，20岁，出生于中国南方的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball,travel";
            row[11] = "2000-09-01";
            row[12] = 299;
            row[13] = 2999;
            row[14] = 82;
            row[15] = 88;
            row[16] = 170;
			row[17] = 175;
            row[18] = 135;
            row[19] = 80;
            row[20] = 130;
            row[21] = 1.0;
            row[22] = 1.0;
            row[23] = 1.2;
            row[24] = 1.2;
            row[25] = 2.69;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 113;
            row[1] = "曹飞";
            row[2] = 2008;
            row[3] = false;
            row[4] = "信息工程";
            row[5] = 1;
            row[6] = 1;
            row[7] = new DateTime(2008, 9, 1);
            row[8] = "曹飞，男，20岁，出生于中国北方的一个小山村，毕业于南方科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball";
            row[11] = "2008-09-01";
            row[12] = 399;
            row[13] = 3996;
            row[14] = 96;
            row[15] = 85;
            row[16] = 181;
			row[17] = 175;
            row[18] = 135;
            row[19] = 80;
            row[20] = 130;
            row[21] = 1.0;
            row[22] = 1.0;
            row[23] = 1.2;
            row[24] = 1.2;
            row[25] = 2.69;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 114;
            row[1] = "孙婷婷";
            row[2] = 2001;
            row[3] = true;
            row[4] = "材料物理与化学";
            row[5] = 2;
            row[6] = 0;
            row[7] = new DateTime(2001, 9, 1);
            row[8] = "孙婷婷，女，28岁，出生于中国海南岛的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,basketball,music";
            row[11] = "2001-09-01";
            row[12] = 499;
            row[13] = 4990;
            row[14] = 80;
            row[15] = 90;
            row[16] = 170;
			row[17] = 175;
            row[18] = 135;
            row[19] = 80;
            row[20] = 130;
            row[21] = 1.0;
            row[22] = 1.0;
            row[23] = 1.2;
            row[24] = 1.2;
            row[25] = 2.69;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 115;
            row[1] = "董国";
            row[2] = 2020;
            row[3] = false;
            row[4] = "国际经济与贸易";
            row[5] = 2;
            row[6] = 1;
            row[7] = new DateTime(2008, 9, 1);
            row[8] = "董国，男，22岁，出生于中国澳门的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,music";
            row[11] = "2008-09-01";
            row[12] = 299;
            row[13] = 2992;
            row[14] = 90;
            row[15] = 95;
            row[16] = 185;
			row[17] = 175;
            row[18] = 135;
            row[19] = 80;
            row[20] = 130;
            row[21] = 1.0;
            row[22] = 1.0;
            row[23] = 1.2;
            row[24] = 1.2;
            row[25] = 2.69;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 116;
            row[1] = "习颖颖";
            row[2] = 2020;
            row[3] = true;
            row[4] = "市场营销";
            row[5] = 3;
            row[6] = 0;
            row[7] = new DateTime(2020, 9, 1);
            row[8] = "习颖颖，女，26岁，出生于中国福建的一个小山村，毕业于香港科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,movie,music";
            row[11] = "2020-09-01";
            row[12] = 199;
            row[13] = 1990;
            row[14] = 86;
            row[15] = 90;
            row[16] = 176;
			row[17] = 180;
            row[18] = 155;
            row[19] = 90;
            row[20] = 135;
            row[21] = 1.2;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 117;
            row[1] = "李博";
            row[2] = 2003;
            row[3] = true;
            row[4] = "财务管理";
            row[5] = 3;
            row[6] = 1;
            row[7] = new DateTime(2003, 9, 1);
            row[8] = "李博，男，28岁，出生于中国浙江的一个小山村，毕业于电子科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "movie,music";
            row[11] = "2003-09-01";
            row[12] = 99;
            row[13] = 990;
            row[14] = 80;
            row[15] = 95;
            row[16] = 175;
			row[17] = 180;
            row[18] = 155;
            row[19] = 90;
            row[20] = 135;
            row[21] = 1.2;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 118;
            row[1] = "黄婷婷";
            row[2] = 2000;
            row[3] = false;
            row[4] = "材料物理与化学";
            row[5] = 4;
            row[6] = 0;
            row[7] = new DateTime(2000, 9, 1);
            row[8] = "黄婷婷，女，25岁，出生于中国北方的一个小山村，毕业于北京科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "travel,movie,music";
            row[11] = "2000-09-01";
            row[12] = 399;
            row[13] = 3990;
            row[14] = 65;
            row[15] = 88;
            row[16] = 183;
			row[17] = 180;
            row[18] = 155;
            row[19] = 90;
            row[20] = 135;
            row[21] = 1.2;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 119;
            row[1] = "韩超";
            row[2] = 2020;
            row[3] = false;
            row[4] = "生物医学工程";
            row[5] = 4;
            row[6] = 1;
            row[7] = new DateTime(2020, 9, 1);
            row[8] = "韩超，男，26岁，出生于中国河南的一个小山村，毕业于中国科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "basketball,movie,music";
            row[11] = "2020-09-01";
            row[12] = 399;
            row[13] = 3991;
            row[14] = 88;
            row[15] = 86;
            row[16] = 174;
			row[17] = 180;
            row[18] = 155;
            row[19] = 90;
            row[20] = 135;
            row[21] = 1.2;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 120;
            row[1] = "王娟娟";
            row[2] = 2003;
            row[3] = true;
            row[4] = "材料物理与化学";
            row[5] = 5;
            row[6] = 0;
            row[7] = new DateTime(2003, 9, 1);
            row[8] = "王娟娟，女，25岁，出生于中国广西的一个小山村，毕业于南方科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,travel,movie,music";
            row[11] = "2003-09-01";
            row[12] = 399;
            row[13] = 3992;
            row[14] = 90;
            row[15] = 88;
            row[16] = 178;
			row[17] = 185;
            row[18] = 165;
            row[19] = 85;
            row[20] = 145;
            row[21] = 1.0;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 121;
            row[1] = "周鹏";
            row[2] = 2006;
            row[3] = false;
            row[4] = "电子商务";
            row[5] = 5;
            row[6] = 1;
            row[7] = new DateTime(2006, 9, 1);
            row[8] = "周鹏，男，23岁，出生于中国安徽的一个小山村，毕业于国防科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,movie,music";
            row[11] = "2006-09-01";
            row[12] = 299;
            row[13] = 2992;
            row[14] = 92;
            row[15] = 66;
            row[16] = 186;
			row[17] = 185;
            row[18] = 165;
            row[19] = 85;
            row[20] = 145;
            row[21] = 1.0;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 122;
            row[1] = "吴玲玲";
            row[2] = 2020;
            row[3] = true;
            row[4] = "管理学";
            row[5] = 5;
            row[6] = 0;
            row[7] = new DateTime(2020, 9, 1);
            row[8] = "吴玲玲，女，22岁，出生于中国台湾的一个小山村，毕业于台湾科学技术大学。";
            row[9] = Guid.NewGuid();
            row[10] = "reading,travel,music";
            row[11] = "2020-09-01";
            row[12] = 399;
            row[13] = 3993;
            row[14] = 65;
            row[15] = 89;
            row[16] = 184;
			row[17] = 185;
            row[18] = 165;
            row[19] = 85;
            row[20] = 145;
            row[21] = 1.0;
            row[22] = 1.5;
            row[23] = 1.0;
            row[24] = 1.0;
            row[25] = 2.69;
            table.Rows.Add(row);


            return table;
        }
        #endregion

        #region GetTotalCount GetPagedDataTable

        // 对传入的 pageIndex 进行有效性验证
        public static int ValidatePageIndex(int pageIndex, int pageSize, int recordCount)
        {
            int pageCount = recordCount / pageSize;
            if (recordCount % pageSize != 0)
            {
                pageCount++;
            }
            if (pageIndex > pageCount - 1)
            {
                pageIndex = pageCount - 1;
            }
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }

            return pageIndex;
        }

        /// <summary>
        /// 模拟返回总页数
        /// </summary>
        /// <returns></returns>
        public static int GetPageCount(int pageSize)
        {
            int recordCount = GetTotalCount();
            int pageCount = recordCount / pageSize;
            if (recordCount % pageSize != 0)
            {
                pageCount += 1;
            }
            return pageCount;
        }

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        public static int GetTotalCount()
        {
            return GetTotalCount(String.Empty);
        }

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        public static int GetTotalCount(string searchKeyword)
        {
            DataTable source = DataSourceUtil.GetDataTable2();

            if (!String.IsNullOrEmpty(searchKeyword))
            {
                DataView view2 = source.DefaultView;
                // RowFilter的用法：http://www.csharp-examples.net/dataview-rowfilter/
                view2.RowFilter = String.Format("Name LIKE '*{0}*'", EscapeLikeValue(searchKeyword));

                source = view2.ToTable();
            }

            return source.Rows.Count;
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPagedDataTable(int pageIndex, int pageSize)
        {
            return GetPagedDataTable(pageIndex, pageSize, String.Empty);
        }


        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPagedDataTable(int pageIndex, int pageSize, string searchKeyword)
        {
            DataTable source = DataSourceUtil.GetDataTable2();

            if (!String.IsNullOrEmpty(searchKeyword))
            {
                DataView view2 = source.DefaultView;
                // RowFilter的用法：http://www.csharp-examples.net/dataview-rowfilter/
                view2.RowFilter = String.Format("Name LIKE '*{0}*'", EscapeLikeValue(searchKeyword));
            
                source = view2.ToTable();
            }
            

            pageIndex = ValidatePageIndex(pageIndex, pageSize, source.Rows.Count);


            DataTable paged = source.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > source.Rows.Count)
            {
                rowend = source.Rows.Count;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(source.Rows[i]);
            }

            return paged;
        }

        

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPagedDataTable(int pageIndex, int pageSize, string sortField, string sortDirection)
        {
            return GetPagedDataTable(pageIndex, pageSize, sortField, sortDirection, String.Empty);
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPagedDataTable(int pageIndex, int pageSize, string sortField, string sortDirection, string searchKeyword)
        {
            string[] sortFields = null;
            // 支持取消排序
            if (!String.IsNullOrEmpty(sortField))
            {
                sortFields = new string[] { sortField, sortDirection };
            }
            return GetPagedDataTable(pageIndex, pageSize, sortFields, searchKeyword);
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPagedDataTable(int pageIndex, int pageSize, string[] sortFields)
        {
            return GetPagedDataTable(pageIndex, pageSize, sortFields, String.Empty);
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortFields"></param>
        /// <param name="searchKeyword"></param>
        /// <returns></returns>
        public static DataTable GetPagedDataTable(int pageIndex, int pageSize, string[] sortFields, string searchKeyword)
        {
            // 模拟数据排序
            DataTable source = DataSourceUtil.GetDataTable2();
            DataView view1 = source.DefaultView;

            // 支持取消排序，多列排序
            if (sortFields != null && sortFields.Length > 0)
            {
                List<string> sortItems = new List<string>();
                for (var i = 0; i < sortFields.Length; i += 2)
                {
                    sortItems.Add(String.Format("{0} {1}", sortFields[i], sortFields[i + 1]));
                }
                view1.Sort = String.Join(", ", sortItems);
            }

            if (!String.IsNullOrEmpty(searchKeyword))
            {
                // RowFilter的用法：http://www.csharp-examples.net/dataview-rowfilter/
                view1.RowFilter = String.Format("Name LIKE '*{0}*'", EscapeLikeValue(searchKeyword));
            }

            source = view1.ToTable();


            pageIndex = ValidatePageIndex(pageIndex, pageSize, source.Rows.Count);


            DataTable paged = source.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > source.Rows.Count)
            {
                rowend = source.Rows.Count;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(source.Rows[i]);
            }

            return paged;
        }


        #endregion

        #region GetTreeDataTable

        /// <summary>
        /// 获取模拟树表格
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTreeDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("ParentId", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("Type", typeof(String)));
            table.Columns.Add(new DataColumn("Size", typeof(int)));
            table.Columns.Add(new DataColumn("ModifyDate", typeof(DateTime)));


            DataRow row;

            // basic
            row = table.NewRow();
            row[0] = 50;
            row[1] = -1;
            row[2] = "basic";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-11-03 11:20");
            table.Rows.Add(row);


            // basic -> Captcha
            row = table.NewRow();
            row[0] = 54;
            row[1] = 50;
            row[2] = "Captcha";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-08-17 20:22");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 55;
            row[1] = 54;
            row[2] = "captcha.ashx";
            row[3] = "ASHX文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 56;
            row[1] = 54;
            row[2] = "captcha.ashx.cs";
            row[3] = "CS文件";
            row[4] = 2;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);


            // basic -> hello.aspx
            row = table.NewRow();
            row[0] = 51;
            row[1] = 50;
            row[2] = "hello.aspx";
            row[3] = "ASPX文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 52;
            row[1] = 50;
            row[2] = "hello.aspx.cs";
            row[3] = "CS文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-08-24 11:08");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 53;
            row[1] = 50;
            row[2] = "hello.aspx.designer.cs";
            row[3] = "CS文件";
            row[4] = 2;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);


            // common
            row = table.NewRow();
            row[0] = 200;
            row[1] = -1;
            row[2] = "common";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-08-17 20:22");
            table.Rows.Add(row);


            // common -> menu.xml
            row = table.NewRow();
            row[0] = 201;
            row[1] = 200;
            row[2] = "menu.xml";
            row[3] = "XML文件";
            row[4] = 44;
            row[5] = DateTime.Parse("2014-11-27 17:10");
            table.Rows.Add(row);


            // common -> source.aspx
            row = table.NewRow();
            row[0] = 202;
            row[1] = 200;
            row[2] = "source.aspx";
            row[3] = "ASPX文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 203;
            row[1] = 200;
            row[2] = "source.aspx.cs";
            row[3] = "CS文件";
            row[4] = 3;
            row[5] = DateTime.Parse("2014-08-30 16:31");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 204;
            row[1] = 200;
            row[2] = "source.aspx.designer.cs";
            row[3] = "CS文件";
            row[4] = 2;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);


            // res
            row = table.NewRow();
            row[0] = 60;
            row[1] = -1;
            row[2] = "res";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-08-17 20:30");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 61;
            row[1] = 60;
            row[2] = "css";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-11-16 17:54");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 62;
            row[1] = 61;
            row[2] = "common.css";
            row[3] = "CSS文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-10-30 11:35");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 63;
            row[1] = 60;
            row[2] = "images";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-10-09 15:45");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 64;
            row[1] = 63;
            row[2] = "logo";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-04-26 11:29");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 65;
            row[1] = 64;
            row[2] = "logo.png";
            row[3] = "PNG文件";
            row[4] = 5;
            row[5] = DateTime.Parse("2013-09-11 12:13");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 66;
            row[1] = 64;
            row[2] = "favicon.ico";
            row[3] = "ICO文件";
            row[4] = 18;
            row[5] = DateTime.Parse("2013-09-11 12:13");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 67;
            row[1] = 64;
            row[2] = "favicon.gif";
            row[3] = "GIF文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2013-09-11 12:13");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 68;
            row[1] = 63;
            row[2] = "themes";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-07-10 11:52");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 69;
            row[1] = 68;
            row[2] = "metro_blue.png";
            row[3] = "PNG文件";
            row[4] = 5;
            row[5] = DateTime.Parse("2014-10-10 11:42");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 70;
            row[1] = 68;
            row[2] = "metro_orange.png";
            row[3] = "PNG文件";
            row[4] = 5;
            row[5] = DateTime.Parse("2014-10-10 11:43");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 71;
            row[1] = 68;
            row[2] = "ui_darkness.png";
            row[3] = "PNG文件";
            row[4] = 5;
            row[5] = DateTime.Parse("2014-10-10 11:41");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 72;
            row[1] = 68;
            row[2] = "blank.png";
            row[3] = "PNG文件";
            row[4] = 4;
            row[5] = DateTime.Parse("2013-09-11 12:12");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 73;
            row[1] = 68;
            row[2] = "code.gif";
            row[3] = "GIF文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2013-09-11 12:12");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 74;
            row[1] = 68;
            row[2] = "toolbox.png";
            row[3] = "PNG文件";
            row[4] = 39;
            row[5] = DateTime.Parse("2013-09-11 12:13");
            table.Rows.Add(row);



            row = table.NewRow();
            row[0] = 100;
            row[1] = -1;
            row[2] = "default.aspx";
            row[3] = "ASPX文件";
            row[4] = 31;
            row[5] = DateTime.Parse("2014-11-15 18:44");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 101;
            row[1] = -1;
            row[2] = "default.aspx.cs";
            row[3] = "CS文件";
            row[4] = 13;
            row[5] = DateTime.Parse("2014-10-27 18:44");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = -1;
            row[2] = "default.aspx.designer.cs";
            row[3] = "CS文件";
            row[4] = 12;
            row[5] = DateTime.Parse("2014-10-12 20:57");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 105;
            row[1] = -1;
            row[2] = "Web.config";
            row[3] = "CONFIG文件";
            row[4] = 3;
            row[5] = DateTime.Parse("2014-11-06 20:59");
            table.Rows.Add(row);


            return table;
        }


        #endregion

        #region GetLargeTreeDataTable

        /// <summary>
        /// 获取模拟树表格
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLargeTreeDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("ParentId", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("Type", typeof(String)));
            table.Columns.Add(new DataColumn("Size", typeof(int)));
            table.Columns.Add(new DataColumn("ModifyDate", typeof(DateTime)));

            var rowIdSeed = 10001;

            DataRow row;

            // basic
            row = table.NewRow();
            row[0] = 50;
            row[1] = -1;
            row[2] = "basic";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-11-03 11:20");
            table.Rows.Add(row);


            // basic -> Captcha
            row = table.NewRow();
            row[0] = 54;
            row[1] = 50;
            row[2] = "Captcha";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-08-17 20:22");
            table.Rows.Add(row);

            for (int i = 0; i < 500; i++)
            {
                row = table.NewRow();
                row[0] = rowIdSeed++;
                row[1] = 54;
                row[2] = "Captcha-" + rowIdSeed;
                row[3] = "文件夹";
                row[4] = DBNull.Value;
                row[5] = DateTime.Parse("2014-08-17 20:22");
                table.Rows.Add(row);
            }


            for (int i = 0; i < 500; i++)
            {
                row = table.NewRow();
                row[0] = rowIdSeed++;
                row[1] = 50;
                row[2] = "basic-" + rowIdSeed;
                row[3] = "文件夹";
                row[4] = DBNull.Value;
                row[5] = DateTime.Parse("2014-08-17 20:22");
                table.Rows.Add(row);
            }

            row = table.NewRow();
            row[0] = 55;
            row[1] = 54;
            row[2] = "captcha.ashx";
            row[3] = "ASHX文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 56;
            row[1] = 54;
            row[2] = "captcha.ashx.cs";
            row[3] = "CS文件";
            row[4] = 2;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);


            // basic -> hello.aspx
            row = table.NewRow();
            row[0] = 51;
            row[1] = 50;
            row[2] = "hello.aspx";
            row[3] = "ASPX文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 52;
            row[1] = 50;
            row[2] = "hello.aspx.cs";
            row[3] = "CS文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-08-24 11:08");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 53;
            row[1] = 50;
            row[2] = "hello.aspx.designer.cs";
            row[3] = "CS文件";
            row[4] = 2;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);


            // common
            row = table.NewRow();
            row[0] = 200;
            row[1] = -1;
            row[2] = "common";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-08-17 20:22");
            table.Rows.Add(row);

           

            // common -> menu.xml
            row = table.NewRow();
            row[0] = 201;
            row[1] = 200;
            row[2] = "menu.xml";
            row[3] = "XML文件";
            row[4] = 44;
            row[5] = DateTime.Parse("2014-11-27 17:10");
            table.Rows.Add(row);


            // common -> source.aspx
            row = table.NewRow();
            row[0] = 202;
            row[1] = 200;
            row[2] = "source.aspx";
            row[3] = "ASPX文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 203;
            row[1] = 200;
            row[2] = "source.aspx.cs";
            row[3] = "CS文件";
            row[4] = 3;
            row[5] = DateTime.Parse("2014-08-30 16:31");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 204;
            row[1] = 200;
            row[2] = "source.aspx.designer.cs";
            row[3] = "CS文件";
            row[4] = 2;
            row[5] = DateTime.Parse("2014-07-05 16:31");
            table.Rows.Add(row);


            for (int i = 0; i < 500; i++)
            {
                row = table.NewRow();
                row[0] = rowIdSeed++;
                row[1] = 200;
                row[2] = "common-" + rowIdSeed;
                row[3] = "文件夹";
                row[4] = DBNull.Value;
                row[5] = DateTime.Parse("2014-08-17 20:22");
                table.Rows.Add(row);
            }



            // res
            row = table.NewRow();
            row[0] = 60;
            row[1] = -1;
            row[2] = "res";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-08-17 20:30");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 61;
            row[1] = 60;
            row[2] = "css";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-11-16 17:54");
            table.Rows.Add(row);

            for (int i = 0; i < 500; i++)
            {
                row = table.NewRow();
                row[0] = rowIdSeed++;
                row[1] = 61;
                row[2] = "css-" + rowIdSeed;
                row[3] = "文件夹";
                row[4] = DBNull.Value;
                row[5] = DateTime.Parse("2014-08-17 20:22");
                table.Rows.Add(row);
            }


            row = table.NewRow();
            row[0] = 62;
            row[1] = 61;
            row[2] = "common.css";
            row[3] = "CSS文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2014-10-30 11:35");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 63;
            row[1] = 60;
            row[2] = "images";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-10-09 15:45");
            table.Rows.Add(row);

            for (int i = 0; i < 500; i++)
            {
                row = table.NewRow();
                row[0] = rowIdSeed++;
                row[1] = 63;
                row[2] = "images-" + rowIdSeed;
                row[3] = "文件夹";
                row[4] = DBNull.Value;
                row[5] = DateTime.Parse("2014-08-17 20:22");
                table.Rows.Add(row);
            }


            row = table.NewRow();
            row[0] = 64;
            row[1] = 63;
            row[2] = "logo";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-04-26 11:29");
            table.Rows.Add(row);

            for (int i = 0; i < 500; i++)
            {
                row = table.NewRow();
                row[0] = rowIdSeed++;
                row[1] = 64;
                row[2] = "logo-" + rowIdSeed;
                row[3] = "文件夹";
                row[4] = DBNull.Value;
                row[5] = DateTime.Parse("2014-08-17 20:22");
                table.Rows.Add(row);
            }



            row = table.NewRow();
            row[0] = 65;
            row[1] = 64;
            row[2] = "logo.png";
            row[3] = "PNG文件";
            row[4] = 5;
            row[5] = DateTime.Parse("2013-09-11 12:13");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 66;
            row[1] = 64;
            row[2] = "favicon.ico";
            row[3] = "ICO文件";
            row[4] = 18;
            row[5] = DateTime.Parse("2013-09-11 12:13");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 67;
            row[1] = 64;
            row[2] = "favicon.gif";
            row[3] = "GIF文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2013-09-11 12:13");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 68;
            row[1] = 63;
            row[2] = "themes";
            row[3] = "文件夹";
            row[4] = DBNull.Value;
            row[5] = DateTime.Parse("2014-07-10 11:52");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 69;
            row[1] = 68;
            row[2] = "metro_blue.png";
            row[3] = "PNG文件";
            row[4] = 5;
            row[5] = DateTime.Parse("2014-10-10 11:42");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 70;
            row[1] = 68;
            row[2] = "metro_orange.png";
            row[3] = "PNG文件";
            row[4] = 5;
            row[5] = DateTime.Parse("2014-10-10 11:43");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 71;
            row[1] = 68;
            row[2] = "ui_darkness.png";
            row[3] = "PNG文件";
            row[4] = 5;
            row[5] = DateTime.Parse("2014-10-10 11:41");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 72;
            row[1] = 68;
            row[2] = "blank.png";
            row[3] = "PNG文件";
            row[4] = 4;
            row[5] = DateTime.Parse("2013-09-11 12:12");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 73;
            row[1] = 68;
            row[2] = "code.gif";
            row[3] = "GIF文件";
            row[4] = 1;
            row[5] = DateTime.Parse("2013-09-11 12:12");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 74;
            row[1] = 68;
            row[2] = "toolbox.png";
            row[3] = "PNG文件";
            row[4] = 39;
            row[5] = DateTime.Parse("2013-09-11 12:13");
            table.Rows.Add(row);


            for (int i = 0; i < 500; i++)
            {
                row = table.NewRow();
                row[0] = rowIdSeed++;
                row[1] = 60;
                row[2] = "res-" + rowIdSeed;
                row[3] = "文件夹";
                row[4] = DBNull.Value;
                row[5] = DateTime.Parse("2014-08-17 20:22");
                table.Rows.Add(row);
            }




            row = table.NewRow();
            row[0] = 100;
            row[1] = -1;
            row[2] = "default.aspx";
            row[3] = "ASPX文件";
            row[4] = 31;
            row[5] = DateTime.Parse("2014-11-15 18:44");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 101;
            row[1] = -1;
            row[2] = "default.aspx.cs";
            row[3] = "CS文件";
            row[4] = 13;
            row[5] = DateTime.Parse("2014-10-27 18:44");
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = -1;
            row[2] = "default.aspx.designer.cs";
            row[3] = "CS文件";
            row[4] = 12;
            row[5] = DateTime.Parse("2014-10-12 20:57");
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 105;
            row[1] = -1;
            row[2] = "Web.config";
            row[3] = "CONFIG文件";
            row[4] = 3;
            row[5] = DateTime.Parse("2014-11-06 20:59");
            table.Rows.Add(row);


            return table;
        }


        #endregion

        #region GetCountryTable

        /// <summary>
        /// 获取模拟表格（国家列表）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCountryTable()
        {
            DataTable table = new DataTable();
            DataColumn colID = new DataColumn("Id", typeof(String));
            DataColumn colImage = new DataColumn("Image", typeof(String));
            DataColumn colTitle = new DataColumn("Name", typeof(String));
            DataColumn colDesc = new DataColumn("Desc", typeof(String));
            DataColumn colGroup = new DataColumn("Group", typeof(String));
            table.Columns.Add(colID);
            table.Columns.Add(colImage);
            table.Columns.Add(colTitle);
            table.Columns.Add(colDesc);
            table.Columns.Add(colGroup);

            DataRow row = table.NewRow();
            row[0] = "cn";
            row[1] = "cn";
            row[2] = "中国";
            row[3] = "中国位于东亚，是以华夏文明为主体、中华文化为基础，以汉族为主要民族的统一多民族国家，通用汉语。";
            row[4] = "亚洲";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = "fr";
            row[1] = "fr";
            row[2] = "法国";
            row[3] = "法兰西共和国，简称法国，是一个本土位于西欧的总统共和制国家，海外领土包括南美洲和南太平洋的一些地区。";
            row[4] = "欧洲";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = "us";
            row[1] = "us";
            row[2] = "美国";
            row[3] = "美利坚合众国，简称美国，是由华盛顿哥伦比亚特区、50个州和关岛等众多海外领土组成的联邦共和立宪制国家。";
            row[4] = "美洲";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = "england";
            row[1] = "england";
            row[2] = "英国";
            row[3] = "英国，全称大不列颠及北爱尔兰联合王国，本土位于欧洲大陆西北面的不列颠群岛，被北海、英吉利海峡、凯尔特海、爱尔兰海和大西洋包围。";
            row[4] = "欧洲";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = "it";
            row[1] = "it";
            row[2] = "意大利";
            row[3] = "意大利，全称意大利共和国，是一个欧洲国家，主要由南欧的亚平宁半岛及两个位于地中海中的岛屿西西里岛与萨丁岛所组成。";
            row[4] = "欧洲";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = "ca";
            row[1] = "ca";
            row[2] = "加拿大";
            row[3] = "加拿大，为北美洲最北的国家，西抵太平洋，东迄大西洋，北至北冰洋，南方与美国本土接壤。领土面积为998万平方千米，位居世界第二。";
            row[4] = "美洲";
            table.Rows.Add(row);


            return table;
        }

        /// <summary>
        /// 获取模拟表格（国家列表）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCountryTable2()
        {
            DataTable table = new DataTable();
            DataColumn colID = new DataColumn("Id", typeof(String));
            DataColumn colImage = new DataColumn("Image", typeof(String));
            DataColumn colTitle = new DataColumn("Name", typeof(String));
            DataColumn colDesc = new DataColumn("Desc", typeof(String));
            DataColumn colGroup = new DataColumn("Group", typeof(String));
            table.Columns.Add(colID);
            table.Columns.Add(colImage);
            table.Columns.Add(colTitle);
            table.Columns.Add(colDesc);
            table.Columns.Add(colGroup);

            DataRow row = table.NewRow();
            row[0] = "us2";
            row[1] = "us";
            row[2] = "美国 - 2";
            row[3] = "美利坚合众国，简称美国，是由华盛顿哥伦比亚特区、50个州和关岛等众多海外领土组成的联邦共和立宪制国家。";
            row[4] = "美洲";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = "it2";
            row[1] = "it";
            row[2] = "意大利 - 2";
            row[3] = "意大利，全称意大利共和国，是一个欧洲国家，主要由南欧的亚平宁半岛及两个位于地中海中的岛屿西西里岛与萨丁岛所组成。";
            row[4] = "欧洲";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = "fr2";
            row[1] = "fr";
            row[2] = "法国 - 2";
            row[3] = "法兰西共和国，简称法国，是一个本土位于西欧的总统共和制国家，海外领土包括南美洲和南太平洋的一些地区。";
            row[4] = "欧洲";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = "england2";
            row[1] = "england";
            row[2] = "英国 - 2";
            row[3] = "英国，全称大不列颠及北爱尔兰联合王国，本土位于欧洲大陆西北面的不列颠群岛，被北海、英吉利海峡、凯尔特海、爱尔兰海和大西洋包围。";
            row[4] = "欧洲";
            table.Rows.Add(row);


            return table;
        }

        #endregion

        #region RowFilter

        // From: http://www.csharp-examples.net/dataview-rowfilter/
        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        #endregion
    }
}
