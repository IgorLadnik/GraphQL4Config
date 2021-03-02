using System.Collections.Generic;
using System.Xml;
using GraphQL.Types;
using GraphQlHelperLib;
using ConfigModelLib.Models;

namespace ConfigModelLib.Type
{
    public class LoaderType : ObjectGraphType<Loader>
    {
        public LoaderType()
        {
            Field(l => l.Name);
            Field(l => l.Assembly);
            Field(l => l.Type);
            Field(l => l.Enabled);

            Field<ListGraphType<SourceType>>("sources", resolve: context => 
            {
                var xmlDocument = context.GetXmlDocument();
                var loaderName = context.GetFilterPattern("loader");
                List<Source> lstSource = new();
                foreach (XmlElement sourceType in xmlDocument.GetElementsByTagName("Source"))
                {
                    if ((sourceType.ParentNode as XmlElement).GetAttribute("name") == loaderName)
                    {
                        Source source = new()
                        {
                            Value = sourceType.GetAttribute("value"),
                        };
                        lstSource.Add(source);
                    }
                }
                return lstSource;
            });
        }
    }
}
