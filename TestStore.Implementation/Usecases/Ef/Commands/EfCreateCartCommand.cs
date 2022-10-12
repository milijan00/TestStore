using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Domain;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Validators;

namespace TestStore.Implementation.Usecases.Ef.Commands
{
    public class EfCreateCartCommand : EfBase, ICreateCartCommand
    {
        private CartBaseValidator _validator;
        public EfCreateCartCommand(TestStoreDbContext context, CartBaseValidator validator = null) : base(context)
        {
            _validator = validator;
        }

        public int Id => 32;

        public string Name => "EfCreateCartCommand";

        public bool AdminOnly => false;

        public void Execute(CartDto data)
        {
            var result = this._validator.Validate(data);
            if (!result.IsValid)
            {
                throw new UnprocessableEntityException(result.Errors);
            }
            var previousCart = this.Context.Carts.FirstOrDefault(x => x.UserId == data.UserId.Value && x.IsActive);
            if(previousCart != null)
            {
                previousCart.IsActive = false;
            }

            var cart = new Cart
            {
                UserId = data.UserId.Value
            };
            cart.Products = this.AddProducts(data.Products, cart);

            this.Context.Carts.Add(cart);
            this.Context.SaveChanges();
        }

        private List<CartProduct> AddProducts(IEnumerable<CartProductDto> products, Cart cart)
        {
            if(products == null)
            {
                throw new NullReferenceException("Products can not be added to the cart.");
            }
            return  products.Select(x => new CartProduct
            {
                Chart = cart,
                ProductId = x.Id,
                Quantity = x.Quantity
            }).ToList();
        }
    }
}
