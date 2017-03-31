﻿using System;
using Newtonsoft.Json;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Serialization
{
    public class TypeNameSerializationBinder : SerializationBinder
    {
        public string TypeFormat { get; private set; }

        public TypeNameSerializationBinder(string typeFormat)
        {
            TypeFormat = typeFormat;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            string resolvedTypeName = string.Format(TypeFormat, typeName);
          
            return Type.GetType(resolvedTypeName, true);
        }
    }
}
