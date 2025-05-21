CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE TipoCupom (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL
);

CREATE TABLE Cupom (
    Codigo VARCHAR(50) PRIMARY KEY,
    Ativo BOOLEAN DEFAULT TRUE,
    Porcentagem DECIMAL,
    TipoCupomId INT,
    FOREIGN KEY (TipoCupomId) REFERENCES TipoCupom(Id)
);

CREATE TABLE Cliente (
    Id UUID PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Telefone VARCHAR(20),
    Pontos INT DEFAULT 0
);

CREATE TABLE Endereco (
    Id UUID PRIMARY KEY,
    Cep VARCHAR(10),
    Rua VARCHAR(255),
    Bairro VARCHAR(255),
    Numero VARCHAR(50),
    Cidade VARCHAR(255),
    Uf VARCHAR(2),
    ClienteId UUID,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id)
);

CREATE TABLE Provedor (
	Id SERIAL PRIMARY KEY,
	Nome VARCHAR(50) NOT NULL
);

CREATE TABLE Usuario (
    Id UUID PRIMARY KEY,
    ProvedorId INT,
    ClienteId UUID UNIQUE,
    Senha VARCHAR(255),
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id),
	FOREIGN KEY (ProvedorId) REFERENCES Provedor(Id)
);

CREATE TABLE FormaPagamento (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL
);

CREATE TABLE StatusPedido (
	Id SERIAL PRIMARY KEY,
	Status VARCHAR(50) NOT NULL
);

CREATE TABLE CategoriaProduto (
    Id SERIAL PRIMARY KEY,
    Categoria VARCHAR(255) NOT NULL
);

CREATE TABLE Produto (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Preco DECIMAL NOT NULL,
    Descricao TEXT,
    CategoriaId INT,
    Imagem VARCHAR(255),
	Ativo BOOLEAN
    FOREIGN KEY (CategoriaId) REFERENCES CategoriaProduto(Id)
);

CREATE TABLE Adicional (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Valor DECIMAL NOT NULL
);

CREATE TABLE CategoriaAcompanhamento (
    Id SERIAL PRIMARY KEY,
    Categoria VARCHAR(255) NOT NULL,
    MaximoGratis INT DEFAULT 0
);

CREATE TABLE Acompanhamento (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Valor DECIMAL NOT NULL,
    CategoriaAcompanhamentoId INT,
    FOREIGN KEY (CategoriaAcompanhamentoId) REFERENCES CategoriaAcompanhamento(Id)
);

CREATE TABLE Pedido (
    Id SERIAL PRIMARY KEY,
    EnderecoId UUID,
    ClienteId UUID,
    Comentarios TEXT,
    ValorEntrega DECIMAL,
    Total DECIMAL NOT NULL,
    StatusId INT,
    PontosAcumulados INT DEFAULT 0,
    CodigoCupom VARCHAR(50),
    FormaPagamentoId INT,
    FOREIGN KEY (EnderecoId) REFERENCES Endereco(Id),
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id),
    FOREIGN KEY (StatusId) REFERENCES StatusPedido(Id),
    FOREIGN KEY (CodigoCupom) REFERENCES Cupom(Codigo),
    FOREIGN KEY (FormaPagamentoId) REFERENCES FormaPagamento(Id)
);

CREATE TABLE PedidoXProduto (
    Id SERIAL PRIMARY KEY,
    PedidoId INT,
    ProdutoId INT,
    Comentario TEXT,
    Quantidade INT NOT NULL DEFAULT 1,
    FOREIGN KEY (PedidoId) REFERENCES Pedido(Id),
    FOREIGN KEY (ProdutoId) REFERENCES Produto(Id)
);

CREATE TABLE AdicionalProdutoPedido (
    ProdutoPedidoId INT,
    AdicionalId INT,
    FOREIGN KEY (ProdutoPedidoId) REFERENCES PedidoXProduto(Id),
    FOREIGN KEY (AdicionalId) REFERENCES Adicional(Id)
);

CREATE TABLE AcompanhamentoProdutoPedido (
    ProdutoPedidoId INT,
    AcompanhamentoId INT,
    FOREIGN KEY (ProdutoPedidoId) REFERENCES PedidoXProduto(Id),
    FOREIGN KEY (AcompanhamentoId) REFERENCES Acompanhamento(Id)
);

CREATE TABLE CupomProduto (
    CodigoCupom VARCHAR(50),
    ProdutoId INT,
    PRIMARY KEY (CodigoCupom, ProdutoId),
    FOREIGN KEY (CodigoCupom) REFERENCES Cupom(Codigo),
    FOREIGN KEY (ProdutoId) REFERENCES Produto(Id)
);