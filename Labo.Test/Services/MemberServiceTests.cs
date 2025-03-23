using Labo.Application.DTO;
using Labo.Application.Exceptions;
using Labo.Application.Interfaces;
using Labo.Application.Interfaces.Repositories;
using Labo.Application.Services;
using Labo.Domain.Entities;
using Moq;

namespace Labo.Test.Services
{
    public class MemberServiceTests
    {

        [Fact]
        public void RegisterWithDuplicateEmail_ThrowsDuplicatePropertyException()
        {
            Mock<IMailer> mailerMock = new ();
            
            Mock<IMemberRepository> memberRepositoryMock = new ();

            memberRepositoryMock.Setup(
                m => m.Any(It.IsAny<Func<Member, bool>>())
            ).Returns(true);

            MemberService memberService 
                = new (memberRepositoryMock.Object, mailerMock.Object);

            Assert.Throws<DuplicatePropertyException>(() =>
            {
                memberService.Register(new RegisterMemberDTO
                {
                    Email = "lykhun@gmail.com",
                    Elo = 1200,
                    Gender = Domain.Enums.Gender.Male,
                    BirthDate = DateTime.Now,
                    Username = "Khun"
                });
            });
        }

        [Fact]
        public void RegisterWithDuplicateUsername_ThrowsDuplicatePropertyException()
        {
            Mock<IMailer> mailerMock = new();

            Mock<IMemberRepository> memberRepositoryMock = new();

            memberRepositoryMock.SetupSequence(
                m => m.Any(It.IsAny<Func<Member, bool>>())
            ).Returns(false)
            .Returns(true);

            MemberService memberService
                = new(memberRepositoryMock.Object, mailerMock.Object);

            Assert.Throws<DuplicatePropertyException>(() =>
            {
                memberService.Register(new RegisterMemberDTO
                {
                    Email = "lykhun@gmail.com",
                    Elo = 1200,
                    Gender = Domain.Enums.Gender.Male,
                    BirthDate = DateTime.Now,
                    Username = "Khun"
                });
            });
        }

        [Fact]
        public void RegisterWithoutElo_ReturnMemberWith1200Elo()
        {
            Mock<IMailer> mailerMock = new();

            Mock<IMemberRepository> memberRepositoryMock = new();

            memberRepositoryMock.SetupSequence(
                m => m.Any(It.IsAny<Func<Member, bool>>())
            ).Returns(false)
            .Returns(false);

            Member parameter = new ();

            memberRepositoryMock.Setup(
                m => m.Add(It.IsAny<Member>())
            ).Callback((Member p) => parameter = p).Returns(new Member { Username = "Khun" });

            MemberService memberService
                = new(memberRepositoryMock.Object, mailerMock.Object);

            Member m = memberService.Register(new RegisterMemberDTO
            {
                Email = "lykhun@gmail.com",
                Elo = null,
                Gender = Domain.Enums.Gender.Male,
                BirthDate = DateTime.Now,
                Username = "Khun"
            });
            Assert.Equal(1200, parameter.Elo);
        }

        [Fact]
        public void RegisterMember__ShouldSendEmail()
        {
            Mock<IMailer> mailerMock = new();

            Mock<IMemberRepository> memberRepositoryMock = new();

            memberRepositoryMock.SetupSequence(
                m => m.Any(It.IsAny<Func<Member, bool>>())
            ).Returns(false)
            .Returns(false);

            memberRepositoryMock.Setup(s => s.Add(It.IsAny<Member>()))
                .Returns(new Member
                {
                    Username = "Khun"
                });

            MemberService memberService
                = new(memberRepositoryMock.Object, mailerMock.Object);

            memberService.Register(new RegisterMemberDTO
            {
                Email = "lykhun@gmail.com",
                Elo = 1200,
                Gender = Domain.Enums.Gender.Male,
                BirthDate = DateTime.Now,
                Username = "Khun"
            });

            mailerMock.Verify(
                m => m.Send(
                    It.IsAny<string>(),
                    It.IsAny<string>(), 
                    It.IsAny<string>()
            ), Times.Once);
        }
    }
}
