using MediatR;

namespace ApplicationServices.Commands
{
    public sealed class RegisterInvestmentCommand : IRequest<long>
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
        public long[] CoverageIds { get; init; }
    }
}