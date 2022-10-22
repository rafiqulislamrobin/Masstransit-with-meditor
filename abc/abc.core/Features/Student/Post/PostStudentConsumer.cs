using abc.core.Common;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace abc.core.Features.Student.Post
{
    public class PostStudentConsumer : IConsumer<PostStudentCommand>
    {
        //private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PostStudentConsumer(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<PostStudentCommand> context)
        {
            //var taskUnit = _mapper.Map<PostStudentCommand, Entities.TaskUnit>(context.Message);
            var student = new Entities.Student(context.Message.Id, context.Message.Name);
            await _unitOfWork.Repository<Entities.Student, Guid>().AddAsync(student);

            if (await _unitOfWork.CommitAsync() > 0)
            {
                await context.RespondAsync<IPostStudentResponse>(new
                {
                    IsAdded = true
                });
            }
            else
            {
                await context.RespondAsync<IPostStudentResponse>(new { IsAdded = false });
            }
        }
    }
}
