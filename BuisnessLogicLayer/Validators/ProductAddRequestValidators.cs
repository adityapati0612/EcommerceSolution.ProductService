using Ecommerce.BuisnessLogicLayer.DTO;
using FluentValidation;


namespace Ecommerce.BuisnessLogicLayer.Validators;

public class ProductAddRequestValidators:AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidators()
    {
        //Product Name
        RuleFor(temp=>temp.ProductName).NotEmpty().WithMessage("Product name can't be blank");

        //Category
        RuleFor(temp => temp.Category).IsInEnum().WithMessage("Category name should be correct");

        //UnitPrice
        RuleFor(temp => temp.UnitPrice).InclusiveBetween(0,double.MaxValue).WithMessage($"Unit price should between 0 AND {double.MaxValue}");

        //QuantityInStock
        RuleFor(temp => temp.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"QuantityInStock should between 0 AND {int.MaxValue}");

    }
}
