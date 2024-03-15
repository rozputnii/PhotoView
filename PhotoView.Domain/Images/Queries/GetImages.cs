using MediatR;

namespace PhotoView.Domain.Images.Queries;

public sealed record GetImages(
	int? Page,
	int? Limit
) : IRequest<IEnumerable<ImageInfo>>;