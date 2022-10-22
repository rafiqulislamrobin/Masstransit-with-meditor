using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc.core.Features.Student.Get
{
    public class GetStudentConsumer : IConsumer<IGetStudentQuery>
    {
        public async Task Consume(ConsumeContext<IGetStudentQuery> context)
        {
            var x = context.Message.Id;

            x = Guid.NewGuid();

           await context.RespondAsync( new GetStudentResponse
            {
                Id = x,
            });
        }
    }
}
