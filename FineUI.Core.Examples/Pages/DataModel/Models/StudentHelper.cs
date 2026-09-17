using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class StudentHelper
    {
        public static IList<Student> GetSimpleStudentList()
        {
            return GetSimpleStudentList<Student>();
        }


        public static IList<T> GetSimpleStudentList<T>() where T : Student, new()
        {
            var students = new List<T> { 
                new T {
                    Id= 101,
                    Name= "张萍萍",
                    Gender= 0,
                    EntranceYear= 2000,
                    AtSchool= true,
                    Major= "材料科学与工程系",
                    Group = 1,
                    EntranceDate= DateTime.Parse("2000-09-01"),
                    Hobby = new string[]{"reading","basketball","travel"},
                    Family = new Family {
                        FatherName = "张国栋",
                        MotherName = "李梅"
                    },
                    CurrentStatus = Status.Excellent,
                },
                new T {
                    Id= 102,
                    Name= "陈飞",
                    Gender= 1,
                    EntranceYear= 2000,
                    AtSchool= false,
                    Major= "化学系",
                    Group = 1,
                    EntranceDate= DateTime.Parse("2001-09-01"),
                    Hobby = new string[]{"reading","basketball"},
                    Family = new Family {
                        FatherName = "陈国梁",
                        MotherName = "周兰"
                    },
                    CurrentStatus = Status.Good,
                },
                new T {
                    Id= 103,
                    Name= "董婷婷",
                    Gender= 0,
                    EntranceYear= 2000,
                    AtSchool= true,
                    Major= "化学系",
                    Group = 1,
                    EntranceDate= DateTime.Parse("2008-09-01"),
                    Hobby = new string[]{"reading","basketball","music"},
                    Family = new Family {
                        FatherName = "董辅仁",
                        MotherName = "刘静"
                    },
                    CurrentStatus = Status.Excellent,
                },
                new T {
                    Id= 104,
                    Name= "刘国",
                    Gender= 1,
                    EntranceYear= 2002,
                    AtSchool= false,
                    Major= "化学系",
                    Group = 2,
                    EntranceDate= DateTime.Parse("2002-09-01"),
                    Hobby = new string[]{"reading","music"},
                    Family = new Family {
                        FatherName = "刘房龄",
                        MotherName = "湘采荷"
                    },
                    CurrentStatus = Status.MakeUp,
                },
                new T {
                    Id= 105,
                    Name= "康颖颖",
                    Gender= 0,
                    EntranceYear= 2008,
                    AtSchool= true,
                    Major= "数学系",
                    Group = 2,
                    EntranceDate= DateTime.Parse("2008-09-01"),
                    Hobby = new string[]{"travel","movie","music"},
                    Family = new Family {
                        FatherName = "康有为",
                        MotherName = "陆小妹"
                    },
                    CurrentStatus = Status.Retake,
                },
                new T {
                    Id= 106,
                    Name= "彭博",
                    Gender= 1,
                    EntranceYear= 2008,
                    AtSchool= true,
                    Major= "数学系",
                    Group = 3,
                    EntranceDate= DateTime.Parse("2003-09-01"),
                    Hobby = new string[]{"basketball","movie","music"},
                    Family = new Family {
                        FatherName = "彭起",
                        MotherName = "张慧芝"
                    },
                    CurrentStatus = Status.Retake,
                },
                new T {
                    Id= 107,
                    Name= "黄婷婷",
                    Gender= 0,
                    EntranceYear= 2008,
                    AtSchool= true,
                    Major= "数学系",
                    Group = 3,
                    EntranceDate= DateTime.Parse("2000-09-01"),
                    Hobby = new string[]{"reading","travel","movie","music"},
                    Family = new Family {
                        FatherName = "黄世仁",
                        MotherName = "蔡青澄"
                    },
                    CurrentStatus = Status.Good,
                },
                new T {
                    Id= 108,
                    Name= "唐超",
                    Gender= 1,
                    EntranceYear= 2004,
                    AtSchool= false,
                    Major= "物理系",
                    Group = 4,
                    EntranceDate= DateTime.Parse("2004-09-01"),
                    Hobby = new string[]{"reading","movie","music"},
                    Family = new Family {
                        FatherName = "唐三友",
                        MotherName = "郑可馨"
                    },
                    CurrentStatus = Status.MakeUp,
                },
                new T {
                    Id= 109,
                    Name= "杨婷婷",
                    Gender= 0,
                    EntranceYear= 2004,
                    AtSchool= true,
                    Major= "物理系",
                    Group = 4,
                    EntranceDate= DateTime.Parse("2003-09-01"),
                    Hobby = new string[]{"reading","travel","music"},
                    Family = new Family {
                        FatherName = "杨嘉蒋",
                        MotherName = "唐怡香"
                    },
                    CurrentStatus = Status.Excellent,
                },
                new T {
                    Id= 110,
                    Name= "徐鹏",
                    Gender= 1,
                    EntranceYear= 2002,
                    AtSchool= false,
                    Major= "物理系",
                    Group = 4,
                    EntranceDate= DateTime.Parse("2002-09-01"),
                    Hobby = new string[]{"reading","basketball","movie","travel","music"},
                    Family = new Family {
                        FatherName = "徐侠客",
                        MotherName = "冯美琳"
                    },
                    CurrentStatus = Status.Retake,
                },
                new T {
                    Id= 111,
                    Name= "董国",
                    Gender= 1,
                    EntranceYear= 2012,
                    AtSchool= true,
                    Major= "自动化系",
                    Group = 5,
                    EntranceDate= DateTime.Parse("2006-09-01"),
                    Hobby = new string[]{"reading","basketball"},
                    Family = new Family {
                        FatherName = "董致远",
                        MotherName = "蔡诗茵"
                    },
                    CurrentStatus = Status.Excellent,
                },
                new T {
                    Id= 112,
                    Name= "张三石",
                    Gender= 1,
                    EntranceYear= 2012,
                    AtSchool= true,
                    Major= "材料科学与工程系",
                    Group = 5,
                    EntranceDate= DateTime.Parse("2000-09-01"),
                    Hobby = new string[]{"reading","basketball","music"},
                    Family = new Family {
                        FatherName = "张御风",
                        MotherName = "王语嫣"
                    },
                    CurrentStatus = Status.Good,
                }
            };

            return students;
        }

    }
}