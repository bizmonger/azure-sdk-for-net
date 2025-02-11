﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Text.Json;
using Azure.Core;

namespace Azure.Monitor.Query.Models
{
    [CodeGenModel("queryResults")]
    public partial class LogsQueryResult
    {
        [CodeGenMember("error")]
        private readonly JsonElement _error;

        [CodeGenMember("Statistics")]
        private readonly JsonElement _statistics;

        [CodeGenMember("render")]
        private readonly JsonElement _visualization;

        // TODO: Handle not found
        /// <summary>
        /// Returns the primary result of the query.
        /// </summary>
        public LogsQueryResultTable PrimaryTable => Tables.Single(t => t.Name == "PrimaryResult");

        /// <summary>
        /// Returns the query statistics if the <see cref="LogsQueryOptions.IncludeStatistics"/> is set to <c>true</c>. Null otherwise.
        /// </summary>
        public BinaryData Statistics => _statistics.ValueKind == JsonValueKind.Undefined ? null : new BinaryData(_statistics.ToString());

        /// <summary>
        /// Returns the query visualization if the <see cref="LogsQueryOptions.IncludeVisualization"/> is set to <c>true</c>. Null otherwise.
        /// </summary>
        public BinaryData Visualization => _visualization.ValueKind == JsonValueKind.Undefined ? null : new BinaryData(_visualization.ToString());

        /// <summary>
        /// Get's the error that occured during query processing. The value would be <c>null</c> if the query succeeds.
        /// </summary>
        public ResponseError Error => _error.ValueKind == JsonValueKind.Undefined ? null : JsonSerializer.Deserialize<ResponseError>(_error.GetRawText());
    }
}