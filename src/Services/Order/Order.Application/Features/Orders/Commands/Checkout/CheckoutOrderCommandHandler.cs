using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Persistence;
using Order.Application.Contracts.Services;
using Order.Application.Models;

namespace Order.Application.Features.Orders.Commands.Checkout;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Order>(request);
        var newOrder = await _orderRepository.AddAsync(entity);
        _logger.LogInformation("Order create done: {OrderId}", newOrder.Id);

        // fake sendEmail
        var email = new EmailRequest(){To = "test@gmail.com", Subject = "Checkout", Body = $"Order {newOrder.Id} created"};

        try
        {
            await _emailService.SendEmailAsync(email);
        }
        catch (Exception e)
        {
            _logger.LogError("Error sending email: {Error}", e.Message);
        }
        
        return newOrder.Id;
    }
}