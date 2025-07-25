using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;
using PortaCapena.OdooJsonRpcClient.Shared.Models;
using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace PortaCapena.OdooJsonRpcClient.Tests
{

    public class OdooModelMapperTests
    {
        [Fact]
        public void CanMap_StringToEnum()
        {
            object output;

            /*
             *  In StatusPurchaseOrderOdooEnum we have this declaration :
             *  
             *  [EnumMember(Value = "done")]
             *  Locked = 5,
             *  
             *  So the JValue should be "done" (String) and the output should be Locked
             * */

            OdooModelMapper.ConverOdooPropertyToDotNet(
                typeof(StatusPurchaseOrderOdooEnum),
                new Newtonsoft.Json.Linq.JValue("done"),
                out output
            );

            var result = (StatusPurchaseOrderOdooEnum)output;

            Assert.Equal(StatusPurchaseOrderOdooEnum.Locked, result);
        }


        [Fact]
        public void CanMap_IntegerToEnum()
        {
            object output;

            /*
             *  In PriorityPurchaseOrderOdooEnum we have this declaration :
             *  
             *  [EnumMember(Value = "1")]
             *  Urgent = 2,
             *  
             *  So the JValue should be 1 (Integer) and the output should be Urgent
             * */

            OdooModelMapper.ConverOdooPropertyToDotNet(
                typeof(PriorityPurchaseOrderOdooEnum),
                new Newtonsoft.Json.Linq.JValue(1),
                out output
            );

            var result = (PriorityPurchaseOrderOdooEnum)output;

            Assert.Equal(PriorityPurchaseOrderOdooEnum.Urgent, result);
        }

        [Fact]
        public void CanMap_JsonObjectToString()
        {
            const string json = "{\r\n  \"currency_id\": 1,\r\n  \"currency_pd\": 0.01,\r\n  \"company_currency_id\": 1,\r\n  \"company_currency_pd\": 0.01,\r\n  \"has_tax_groups\": false,\r\n  \"subtotals\": [\r\n    {\r\n      \"tax_groups\": [],\r\n      \"tax_amount_currency\": 0.0,\r\n      \"tax_amount\": 0.0,\r\n      \"base_amount_currency\": 462.5,\r\n      \"base_amount\": 462.5,\r\n      \"name\": \"Montant hors taxes\"\r\n    }\r\n  ],\r\n  \"base_amount_currency\": 462.5,\r\n  \"base_amount\": 462.5,\r\n  \"tax_amount_currency\": 0.0,\r\n  \"tax_amount\": 0.0,\r\n  \"same_tax_base\": true,\r\n  \"total_amount_currency\": 462.5,\r\n  \"total_amount\": 462.5\r\n}";

            var jObject = JObject.Parse(json);
            Assert.Equal(JTokenType.Object, jObject.Type);
            OdooModelMapper.ConverOdooPropertyToDotNet(
                typeof(string),
                jObject,
                out var output
            );

            Assert.Equal(json, output);
        }
    }
}