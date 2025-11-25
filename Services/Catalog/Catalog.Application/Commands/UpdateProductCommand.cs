using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }

        public decimal Price { get; set; }

        public string ImageFile { get; set; }

        public ProductBrand Brand { get; set; }

        public ProductType Type { get; set; }

        public UpdateProductCommand(
            string id,
            string name,
            string description,
            string summary,
            decimal price,
            string imageFile,
            ProductBrand brand,
            ProductType type)
        {
            Id = id;
            Name = name;
            Description = description;
            Summary = summary;
            Price = price;
            ImageFile = imageFile;
            Brand = brand;
            Type = type;
        }
    }
}

