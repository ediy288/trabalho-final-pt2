using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uniplac.Avaliacao.Dominio.Entidades;
using Uniplac.Avaliacao.Infra.Dados.Contexto;
using Uniplac.Avaliacao.Infra.Dados.Repositorios;
using Uniplac.Avaliacao.Testes.Base;

namespace Uniplac.Avaliacao.Testes.InfraTestes
{
    [TestClass]
    public class AlugaRepositorioTeste
    {

        private LojaContexto _contextoTeste;
        private AlugaRepositorio _repositorio;
        private Aluga _alugaTest;

        [TestInitialize]
        public void Inicializador()
        {

            Database.SetInitializer(new InicializadorBanco<LojaContexto>());

            _contextoTeste = new LojaContexto();

            _repositorio = new AlugaRepositorio(_contextoTeste);

            _alugaTest = ConstrutorObjeto.CriarAluguel();

            _contextoTeste.Database.Initialize(true);
        }

        [TestMethod]
        public void Deveria_adicionar_um_novo_Aluguel()
        {
           
            _repositorio.Adicionar(_alugaTest);

            //Afirmar
            var todosAlugueis = _contextoTeste.Alugas.ToList();

            Assert.AreEqual(2, todosAlugueis.Count);
        }

        [TestMethod]
        public void Deveria_editar_um_aluguel()
        {
            //Preparação
            var clienteEditado = _contextoTeste.Alugas.Find(1);
            clienteEditado.QuantDia = 1;

            //Ação
            _repositorio.Editar(clienteEditado);

            //Afirmar
            var clienteBuscado = _contextoTeste.Alugas.Find(1);

            Assert.AreEqual(1, clienteBuscado.QuantDia);
        }


        [TestMethod]
        public void Deveria_deletar_um_aluguel()
        {
            //Preparação
            var aluguelDeletado = _contextoTeste.Alugas.Find(1);

            //Ação
            _repositorio.Deletar(aluguelDeletado);

            //Afirmar
            var todosAlugueis = _contextoTeste.Alugas.ToList();

            Assert.AreEqual(0, todosAlugueis.Count);
        }

        [TestMethod]
        public void Deveria_buscar_aluga_por_id()
        {

        
            var aluguelBuscado = _repositorio.BuscarPor(1);

            Assert.IsNotNull(aluguelBuscado);
        }

        [TestMethod]
        public void Deveria_buscar_todos_os_alugueis()
        {
            //Preparação

            //Ação
            var aluguelBuscado = _repositorio.BuscarTudo();

            //Afirmar

            Assert.IsNotNull(aluguelBuscado);
        }

        [TestMethod]
        public void Deveria_buscar_Alugar_por_valor()
        {
            //Preparação

            //Ação
            var aluguelBuscado = _repositorio.BuscarPorValor(11000);

            //Afirmar

            Assert.IsNotNull(aluguelBuscado);
        }
        
    }
}
