using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    /// <summary>
    /// 学生编辑的内存数据源：用一个静态列表模拟「数据库 + 按主键读取/更新」。
    ///
    /// <para>
    /// <b>Find 刻意返回副本而不是列表里的对象本身</b>，为的是让这个内存数据源的行为和真实数据库/ORM 一致——
    /// 后者每次查询都给你一个新对象，而静态列表是唯一会把同一个对象反复递出去的数据源。若返回活引用，
    /// 事件处理器里的 stored.Name = ... 就直接改了列表里的记录，随后的 Update 退化成「把同一个引用再放回原位」
    /// 的空操作——示例演示的「读取 → 覆盖 → 写回」三步里，第三步就成了摆设。
    /// </para>
    ///
    /// <para>
    /// 示例的「读取 → 覆盖 → 写回」横跨两次加锁（Find 一次、Update 一次），所以并发保存同一条记录时
    /// 后写的会覆盖先写的（丢失更新）。这是 read-first 范式在没有版本列 / 乐观锁时的固有代价；
    /// 真实项目请加行版本或用数据库事务，本示例不引入这层复杂度。
    /// </para>
    /// </summary>
    public static class StudentStore
    {
        private static readonly object _lock = new object();
        private static readonly List<Student> _students = new List<Student>(StudentHelper.GetSimpleStudentList());

        /// <summary>
        /// 按主键读取一份副本（不存在返回 null）。
        /// </summary>
        public static Student Find(int id)
        {
            lock (_lock)
            {
                var found = _students.FirstOrDefault(s => s.Id == id);
                return found == null ? null : CopyOf(found);
            }
        }

        /// <summary>
        /// 按主键写回（不存在则追加，模拟「新增」分支）。
        /// </summary>
        public static void Update(Student edited)
        {
            lock (_lock)
            {
                for (int i = 0; i < _students.Count; i++)
                {
                    if (_students[i].Id == edited.Id)
                    {
                        _students[i] = CopyOf(edited);
                        return;
                    }
                }
                _students.Add(CopyOf(edited));
            }
        }

        // 逐属性复制（模拟从数据库读出一个新对象）
        private static Student CopyOf(Student source)
        {
            return new Student
            {
                Id = source.Id,
                Name = source.Name,
                Gender = source.Gender,
                EntranceYear = source.EntranceYear,
                AtSchool = source.AtSchool,
                Major = source.Major,
                Group = source.Group,
                EntranceDate = source.EntranceDate,
                Hobby = source.Hobby == null ? null : (string[])source.Hobby.Clone(),
                Family = CopyOf(source.Family),
                Score = CopyOf(source.Score),
                CurrentStatus = source.CurrentStatus
            };
        }

        // 嵌套对象同样复制——否则「返回副本」的承诺对 Family 不成立，改副本会写穿数据源
        private static Family CopyOf(Family source)
        {
            if (source == null)
            {
                return null;
            }
            return new Family { FatherName = source.FatherName, MotherName = source.MotherName };
        }

        // Score 也是可变类，同样要复制：漏了它，事件处理器里改 stored.Score.Chinese 就直接写穿静态数据源，
        // 「返回副本」的承诺对成绩不成立（当前示例数据没填 Score，但这道口子一填就漏）
        private static Score CopyOf(Score source)
        {
            if (source == null)
            {
                return null;
            }
            return new Score
            {
                Chinese = source.Chinese,
                Math = source.Math,
                Physics = source.Physics,
                Chemistry = source.Chemistry
            };
        }
    }
}
