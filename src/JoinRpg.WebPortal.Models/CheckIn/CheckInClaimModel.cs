using System.ComponentModel.DataAnnotations;
using JoinRpg.DataModel;
using JoinRpg.Domain;
using JoinRpg.Domain.Problems;
using JoinRpg.Helpers;
using JoinRpg.Markdown;
using JoinRpg.PrimitiveTypes.ProjectMetadata;
using JoinRpg.Web.Models.Characters;
using JoinRpg.Web.Models.Print;

namespace JoinRpg.Web.Models.CheckIn;

public class CheckInClaimModel : IProjectIdAware
{
    public CheckInClaimModel(Claim claim,
        User currentUser,
        IReadOnlyCollection<PlotElement>? plotElements,
        IProblemValidator<Claim> claimValidator,
        ProjectInfo projectInfo)
    {
        if (claim == null)
        {
            throw new ArgumentNullException(nameof(claim));
        }

        if (currentUser == null)
        {
            throw new ArgumentNullException(nameof(currentUser));
        }

        Validator = new ClaimCheckInValidator(claim, claimValidator, projectInfo);
        CheckInTime = claim.CheckInDate;
        ClaimStatus = (ClaimStatusView)claim.ClaimStatus;
        PlayerDetails = new UserProfileDetailsViewModel(claim.Player, (AccessReason)claim.Player.GetProfileAccess(currentUser));
        Navigation = CharacterNavigationViewModel.FromClaim(claim, currentUser.UserId, CharacterNavigationPage.None);

        CanAcceptFee = claim.Project.CanAcceptCash(currentUser);
        ClaimId = claim.ClaimId;
        ProjectId = claim.ProjectId;
        Master = claim.ResponsibleMasterUser;
        Handouts =
          plotElements?.Where(e => e.ElementType == PlotElementType.Handout && e.IsActive)
            .Select(e => e.PublishedVersion())
            .WhereNotNull()
            .Select(e => new HandoutListItemViewModel(e.Content.ToPlainText(), e.AuthorUser))
            .ToArray() ?? Array.Empty<HandoutListItemViewModel>();
        NotFilledFields = Validator.NotFilledFields
          .Select(frp => new NotFilledFieldViewModel(
            frp.Field.CanPlayerEdit
              ? NotFilledFieldViewModel.WhoWllFillEnum.Player
              : NotFilledFieldViewModel.WhoWllFillEnum.Master, frp.Field.Name)).ToList();

        CurrentUserFullName = currentUser.FullName;


    }
    public ClaimCheckInValidator Validator { get; }
    [UIHint("EventTime")]
    public DateTime? CheckInTime { get; }
    public ClaimStatusView ClaimStatus { get; }
    public UserProfileDetailsViewModel PlayerDetails { get; }
    public CharacterNavigationViewModel Navigation { get; }
    public bool CanAcceptFee { get; }
    public int ClaimId { get; }
    public int ProjectId { get; }
    [Display(Name = "Ответственный мастер")]
    public User Master { get; }
    public IReadOnlyCollection<NotFilledFieldViewModel> NotFilledFields { get; }
    public IReadOnlyCollection<HandoutListItemViewModel> Handouts { get; }
    public string CurrentUserFullName { get; }
}

public class NotFilledFieldViewModel
{
    public NotFilledFieldViewModel(WhoWllFillEnum whoWillFill, string fieldName)
    {
        WhoWillFill = whoWillFill;
        FieldName = fieldName;
    }

    public WhoWllFillEnum WhoWillFill { get; }
    public string FieldName { get; }

    public enum WhoWllFillEnum
    {
        [Display(Name = "Мастер")]
        Master,
        [Display(Name = "Игрок")]
        Player,
    }
}
