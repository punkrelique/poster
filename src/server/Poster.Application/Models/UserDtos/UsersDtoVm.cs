using Poster.Domain;

namespace Poster.Application.Models.UserDtos;

public class GetUsersDtoVm
{
    public List<GetUserDto> Users { get;  }

    public GetUsersDtoVm(List<User> users)
    {
        Users = users.Select(u => new GetUserDto(u)).ToList();
    }
}