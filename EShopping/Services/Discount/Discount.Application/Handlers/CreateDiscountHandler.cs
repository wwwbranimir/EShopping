﻿using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers
{
    public class CreateDiscountHandler:IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository discountRepository;
        private readonly IMapper mapper;

        public CreateDiscountHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            this.discountRepository = discountRepository;
            this.mapper = mapper;
        }
        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            //TODO: refactor auto mapper
           var coupon = mapper.Map<Coupon>(request);
            await discountRepository.CreateDiscount(coupon);
            return mapper.Map<CouponModel>(coupon);

           
        }
    }
}
