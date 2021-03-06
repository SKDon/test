﻿using System;
using System.Collections.Concurrent;
using System.Data;
using Dapper;

namespace Alicargo.DataAccess.Helpers
{
	internal sealed class TableParameters : SqlMapper.IDynamicParameters
	{
		private static readonly ConcurrentDictionary<Type, Action<IDbCommand, object>> ParamInfoGenerators =
			new ConcurrentDictionary<Type, Action<IDbCommand, object>>();

		private readonly object _parameters;
		private readonly DataTable[] _tables;

		public TableParameters(object parameters, params DataTable[] tables)
		{
			_parameters = parameters;

			_tables = tables;
		}

		public TableParameters(params DataTable[] tables)
		{
			_tables = tables;
		}

		public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
		{
			if (_parameters != null)
			{
				AddParameters(command, identity, _parameters);
			}

			foreach (var table in _tables)
			{
				var parameter = command.CreateParameter();

				parameter.ParameterName = table.TableName;
				parameter.Value = table;

				command.Parameters.Add(parameter);
			}
		}

		private static void AddParameters(IDbCommand command, SqlMapper.Identity identity, object parameters)
		{
			var paramInfoGenerator = ParamInfoGenerators.GetOrAdd(parameters.GetType(), type =>
			{
				var index = identity.ForDynamicParameters(type);

				return SqlMapper.CreateParamInfoGenerator(index, true, true);
			});

			paramInfoGenerator(command, parameters);
		}

		internal static DataTable GeIdsTable(string name, long[] ids)
		{
			var table = new DataTable(name);
			table.Columns.Add("Id", typeof(long));
			foreach (var id in ids)
			{
				table.Rows.Add(id);
			}

			return table;
		}
	}
}