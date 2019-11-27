﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SandwichSystem.BusinessLayer.UseCases.Assistante;
using SandwichSystem.DataLayer.Interfaces;
using SandwichSystem.Shared.BTO;
using SandwichSystem.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandwichSystem.BusinessLayerTests.UseCases.AssistanteTests
{
    [TestClass]
    public class Supplier_UpdateSupplierTests
    {
        [TestMethod]
        public void UpdateSupplier_ThrowException_WhenSupplierIDisDifferentOfZero()
        {
            //ARRANGE
            var AssistanteRole = new Assistante((new Mock<IUnitOfWork>()).Object);
            var SupplierToUpdate = new SupplierBTO { Id = 0, Name = "InexistantSupplier" };

            //ACT
            Assert.ThrowsException<Exception>( () => AssistanteRole.UpdateSupplier(SupplierToUpdate));
        }

        [TestMethod]
        public void UpdateSupplier_ThrowException_WhenSupplierIsNull()
        {
            //ARRANGE
            var AssistanteRole = new Assistante((new Mock<IUnitOfWork>()).Object);

            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => AssistanteRole.UpdateSupplier(null));
        }

        [TestMethod]
        public void UpdateSupplier_ReturnsTrue_WhenAValidSupplierIsProvidedAndUpdatedInDB()
        {
            //ARRANGE
            var mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(x => x.Update(It.IsAny<SupplierDTO>()));

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(x => x.SupplierRepository).Returns(mockSupplierRepository.Object);

            var AssistanteRole = new Assistante(mockUoW.Object);
            var SupplierToUpdate = new SupplierBTO { Id = 10, Name = "ExistantSupplier" };

            //ACT
            var ReturnValueToAssert = AssistanteRole.UpdateSupplier(SupplierToUpdate);

            Assert.IsTrue(ReturnValueToAssert);
        }

        [TestMethod]
        public void UpdateSupplier_SupplierRepositoryIsCalledOnce_WhenAValidSupplierIsProvidedAndUpdatedInDB()
        {
            //ARRANGE
            var mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(x => x.Update(It.IsAny<SupplierDTO>()));

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(x => x.SupplierRepository).Returns(mockSupplierRepository.Object);

            var AssistanteRole = new Assistante(mockUoW.Object);
            var SupplierToUpdate = new SupplierBTO { Id = 10, Name = "ExistantSupplier" };

            //ACT
            AssistanteRole.UpdateSupplier(SupplierToUpdate);

            mockSupplierRepository.Verify(x => x.Update(It.IsAny<SupplierDTO>()), Times.Once);
        }
    }
}
