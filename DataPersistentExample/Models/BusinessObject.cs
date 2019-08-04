using Dapper.Contrib.Extensions;
using System;

namespace DataPersistentExample.Models
{
    /// <summary>
    /// 业务对象
    /// </summary>
    public class BusinessObject
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ExplicitKey]//Dapper.Contrib需要手动指定主键
        public Guid Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
