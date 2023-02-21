using AutoMapper;
using NetKubernetes.Dtos.InterviewProgram;
using NetKubernetes.Models;

namespace NetKubernetes.Profiles;

public class InterviewProfile : Profile {

    public InterviewProfile()
    {
        CreateMap<Candidate, CandidateResponseDto>();
        CreateMap<CandidateRequestDto, Candidate>();
        CreateMap<Interviewer, InterviewerResponseDto>();
        CreateMap<InterviewerRequestDto, Interviewer>();
        CreateMap<Questionary, QuestionaryResponseDto>();
        CreateMap<QuestionaryRequestDto, Questionary>();
        CreateMap<Topic, TopicResponseDto>();
        CreateMap<TopicRequestDto, Topic>();
        CreateMap<Interview, InterviewResponseDto>();
        CreateMap<InterviewRequestDto, Interview>();
        CreateMap<InterviewTopic, InterviewTopicResponseDto>();
        CreateMap<InterviewTopicRequestDto, InterviewTopic>();
    }

}