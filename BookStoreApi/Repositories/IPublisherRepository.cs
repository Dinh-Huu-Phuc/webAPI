﻿using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
namespace WebAPI.Repositories
{
    public interface IPublisherRepository
    {
        List<PublisherDTO> GetAllPublishers();
        PublisherNoIdDTO GetPublisherById(int id);
        AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
        Publishers? DeletePublisherById(int id);
        bool ExistsByName(string name);
        bool ExistsById(int id);

        bool ExistsByNameExcludingId(string name, int excludeId);
        bool HasBooks(int publisherId);
    }
}
