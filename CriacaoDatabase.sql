-- Criar database UMIO antes de executar script

-- Tabela TipoCupom
CREATE TABLE TipoCupom (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL
);

-- Tabela Cupom
CREATE TABLE Cupom (
    Codigo VARCHAR(50) PRIMARY KEY,
    Ativo BOOLEAN DEFAULT TRUE,
    Porcentagem DECIMAL,
    TipoCupomId INT,
    FOREIGN KEY (TipoCupomId) REFERENCES TipoCupom(Id)
);

-- Tabela Cliente
CREATE TABLE Cliente (
    Id UUID PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Telefone VARCHAR(20),
    Pontos INT DEFAULT 0
);

-- Tabela Endereco
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

CREATE TABLE Provedor(
	Id SERIAL PRIMARY KEY,
	Provedor VARCHAR(50)
);

-- Tabela Usuario
CREATE TABLE Usuario (
    Id UUID PRIMARY KEY,
    ProvedorId INT,
    ClienteId UUID,
    Senha VARCHAR(255) NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id),
	FOREIGN KEY (ProvedorId) REFERENCES Provedor(Id)
);

-- Tabela FormaPagamento
CREATE TABLE FormaPagamento (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL
);

-- Tabela dos status possíveis do pedido
CREATE TABLE StatusPedido (
	Id SERIAL PRIMARY KEY,
	Status VARCHAR(50)
);

-- Tabela Pedido
CREATE TABLE Pedido (
    Id INT PRIMARY KEY,
    EnderecoId UUID,
    ClienteId UUID,
    Comentarios TEXT,
    ValorEntrega DECIMAL,
    Total DECIMAL NOT NULL,
    Status INT,
    PontosAcumulados INT DEFAULT 0,
    CodigoCupom VARCHAR(50),
    FormaPagamentoId INT,
    FOREIGN KEY (EnderecoId) REFERENCES Endereco(Id),
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id),
    FOREIGN KEY (CodigoCupom) REFERENCES Cupom(Codigo),
    FOREIGN KEY (FormaPagamentoId) REFERENCES FormaPagamento(Id),	
	FOREIGN KEY (Status) REFERENCES StatusPedido(Id)
);


-- Tabela Produto
CREATE TABLE Produto (
    Id INT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Preco DECIMAL NOT NULL,
    Descricao TEXT,
    Categoria VARCHAR(255),
    Imagem VARCHAR(255),
    TipoProduto VARCHAR(50),
    MelhoriasAdicionais TEXT,
    MelhoriasMolhos TEXT,
    MelhoriasAcompanhamentos TEXT
);

-- Tabela CupomProduto (Tabela Associativa para a relação N:N entre Cupom e Produto)
CREATE TABLE CupomProduto (
    CodigoCupom VARCHAR(50),
    ProdutoId INT,
    FOREIGN KEY (CodigoCupom) REFERENCES Cupom(Codigo),
    FOREIGN KEY (ProdutoId) REFERENCES Produto(Id)
);

-- Tabela PedidoProduto (Tabela Associativa para a relação N:N entre Pedido e Produto)
CREATE TABLE PedidoProduto (
    PedidoId INT,
    ProdutoId INT,
    Comentario TEXT,
    FOREIGN KEY (PedidoId) REFERENCES Pedido(Id),
    FOREIGN KEY (ProdutoId) REFERENCES Produto(Id)
);