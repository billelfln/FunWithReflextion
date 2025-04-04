﻿using System;

namespace FunWithReflextion.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; }

        public TableNameAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
