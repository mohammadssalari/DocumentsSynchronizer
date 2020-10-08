using System;
using System.Data;

namespace DocumentsSynchronizer.DbAccess
{
    public class SqlParam
    {
        /// <summary>
        ///     The Name The Parameter in SQL Script is named without @
        /// </summary>
        public string Name;

        /// <summary>
        ///     The Value of The Object
        /// </summary>
        public object Value;

        /// <summary>
        ///     SQL DataType int32 or DateTime or DateTime2
        /// </summary>
        public DbType ValueType;

        public SqlParam(string name, object value, DbType valueType)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            ValueType = valueType;
        }
    }
}