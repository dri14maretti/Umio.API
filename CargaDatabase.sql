-- Script de Carga Base para o banco UMIO
-- Assumes que a extensão uuid-ossp está habilitada e as tabelas foram criadas

-- Inserir dados na tabela TipoCupom
INSERT INTO TipoCupom (Nome) VALUES
('Porcentagem'),
('Valor Fixo'),
('Frete Grátis');

-- Inserir dados na tabela Cupom
-- Note: Codigo é a chave primária
INSERT INTO Cupom (Codigo, Ativo, Porcentagem, TipoCupomId) VALUES
('PRIMEIRACOMPRA15', TRUE, 15.00, (SELECT Id FROM TipoCupom WHERE Nome = 'Porcentagem')),
('MAIS5OFF', TRUE, NULL, (SELECT Id FROM TipoCupom WHERE Nome = 'Valor Fixo')),
('FRETEZERO', TRUE, NULL, (SELECT Id FROM TipoCupom WHERE Nome = 'Frete Grátis'));

-- Inserir dados na tabela Cliente
-- Usando gen_random_uuid() para os IDs, conforme a estrutura da tabela
INSERT INTO Cliente (Id, Nome, Email, Telefone, Pontos) VALUES
('a1b2c3d4-e5f6-7890-1234-567890abcdef', 'João Santos', 'joao.santos@email.com', '11912345678', 100),
('b2c3d4e5-f6a7-8901-2345-67890abcdef1', 'Maria Oliveira', 'maria.oliver@email.com', '21987654321', 250),
('c3d4e5f6-a7b8-9012-3456-7890abcdef23', 'Pedro Almeida', 'pedro.almeida@email.com', '31999887766', 50);

-- Obter IDs de clientes inseridos (UUIDs) para usar como chaves estrangeiras
-- Em um script real, você pode capturar esses IDs após a inserção ou gerá-los antes.
-- Para simplificar, vamos re-selecionar.
-- É mais robusto em produção usar RETURNING ou variáveis/CTE.
DO $$
DECLARE
    joao_id UUID := 'a1b2c3d4-e5f6-7890-1234-567890abcdef';
    maria_id UUID := 'b2c3d4e5-f6a7-8901-2345-67890abcdef1';
    pedro_id UUID := 'c3d4e5f6-a7b8-9012-3456-7890abcdef23';
BEGIN
    -- Inserir dados na tabela Endereco
    INSERT INTO Endereco (Id, Cep, Rua, Bairro, Numero, Cidade, Uf, ClienteId) VALUES
    (gen_random_uuid(), '01001-000', 'Rua da Paz', 'Centro', '150', 'Sao Paulo', 'SP', joao_id),
    (gen_random_uuid(), '20040-001', 'Av. Central', 'Centro', '500 Bl 2', 'Rio de Janeiro', 'RJ', maria_id),
    (gen_random_uuid(), '30130-000', 'Av. Afonso Pena', 'Centro', '1000 Ap 301', 'Belo Horizonte', 'MG', pedro_id);

    -- Inserir dados na tabela Provedor
    INSERT INTO Provedor (Nome) VALUES
    ('Email/Senha'),
    ('Google'),
    ('Facebook');

    -- Inserir dados na tabela Usuario
    INSERT INTO Usuario (Id, ProvedorId, ClienteId, Senha) VALUES
    (gen_random_uuid(), (SELECT Id FROM Provedor WHERE Nome = 'Email/Senha'), joao_id, 'hash_senha_joao'),
    (gen_random_uuid(), (SELECT Id FROM Provedor WHERE Nome = 'Google'), maria_id, NULL); -- Maria usa login via Google

END $$;


-- Inserir dados na tabela FormaPagamento
INSERT INTO FormaPagamento (Nome) VALUES
('Cartão de Crédito'),
('PIX'),
('Dinheiro na Entrega');

-- Inserir dados na tabela StatusPedido
INSERT INTO StatusPedido (Status) VALUES
('Aguardando Pagamento'),
('Em Preparação'),
('Saiu para Entrega'),
('Entregue'),
('Cancelado');

-- Inserir dados na tabela CategoriaProduto
INSERT INTO CategoriaProduto (Categoria) VALUES
('Pizzas'),
('Hamburgueres'),
('Bebidas'),
('Sobremesas');

-- Inserir dados na tabela Produto
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem, TipoProduto) VALUES
('Pizza Calabresa', 55.00, 'Pizza de calabresa com cebola e mussarela', (SELECT Id FROM CategoriaProduto WHERE Categoria = 'Pizzas'), 'pizza_calabresa.jpg', 'Principal'),
('Duplo Cheeseburger', 35.90, 'Dois hamburgueres, queijo, alface, tomate', (SELECT Id FROM CategoriaProduto WHERE Categoria = 'Hamburgueres'), 'duplo_cheeseburger.jpg', 'Principal'),
('Refrigerante Lata', 7.00, 'Lata de 350ml', (SELECT Id FROM CategoriaProduto WHERE Categoria = 'Bebidas'), 'refri_lata.jpg', 'Bebida'),
('Petit Gateau', 20.00, 'Bolo quente com sorvete', (SELECT Id FROM CategoriaProduto WHERE Categoria = 'Sobremesas'), 'petit_gateau.jpg', 'Sobremesa');

-- Inserir dados na tabela Adicional
INSERT INTO Adicional (Nome, Valor) VALUES
('Bacon Extra', 5.00),
('Cheddar Cremoso', 6.00),
('Borda Recheada Catupiry', 10.00);

-- Inserir dados na tabela CategoriaAcompanhamento
INSERT INTO CategoriaAcompanhamento (Categoria, MaximoGratis) VALUES
('Batatas Fritas', 1), -- 1 porção de batata frita grátis por lanche
('Molhos', 3); -- 3 molhos grátis por pedido

-- Inserir dados na tabela Acompanhamento
INSERT INTO Acompanhamento (Nome, Valor, CategoriaAcompanhamentoId) VALUES
('Batata Frita Pequena', 8.00, (SELECT Id FROM CategoriaAcompanhamento WHERE Categoria = 'Batatas Fritas')),
('Batata Frita Média', 12.00, (SELECT Id FROM CategoriaAcompanhamento WHERE Categoria = 'Batatas Fritas')),
('Ketchup', 0.00, (SELECT Id FROM CategoriaAcompanhamento WHERE Categoria = 'Molhos')),
('Maionese', 0.00, (SELECT Id FROM CategoriaAcompanhamento WHERE Categoria = 'Molhos')),
('Molho Barbecue', 1.50, (SELECT Id FROM CategoriaAcompanhamento WHERE Categoria = 'Molhos'));

-- Inserir dados na tabela Pedido
-- Usando o bloco DO $$ para facilitar a obtenção de IDs de clientes/endereços
DO $$
DECLARE
    joao_id UUID := 'a1b2c3d4-e5f6-7890-1234-567890abcdef';
    maria_id UUID := 'b2c3d4e5-f6a7-8901-2345-67890abcdef1';
    endereco_joao_id UUID := (SELECT Id FROM Endereco WHERE ClienteId = joao_id LIMIT 1);
    endereco_maria_id UUID := (SELECT Id FROM Endereco WHERE ClienteId = maria_id LIMIT 1);
    cartao_credito_fp INT := (SELECT Id FROM FormaPagamento WHERE Nome = 'Cartão de Crédito');
    pix_fp INT := (SELECT Id FROM FormaPagamento WHERE Nome = 'PIX');
    status_entregue INT := (SELECT Id FROM StatusPedido WHERE Status = 'Entregue');
    status_preparacao INT := (SELECT Id FROM StatusPedido WHERE Status = 'Em Preparação');
BEGIN
    INSERT INTO Pedido (ClienteId, EnderecoId, Comentarios, ValorEntrega, Total, StatusId, PontosAcumulados, CodigoCupom, FormaPagamentoId) VALUES
    (joao_id, endereco_joao_id, 'Tocar campainha forte', 10.00, 72.00, status_entregue, 72, 'PRIMEIRACOMPRA15', cartao_credito_fp), -- Pedido 1
    (maria_id, endereco_maria_id, NULL, 12.00, 47.90, status_preparacao, 0, NULL, pix_fp); -- Pedido 2

    -- Obter IDs dos pedidos inseridos
    -- Novamente, usando SELECTs simples; usar RETURNING seria mais eficiente
    -- Assumindo que os IDs gerados automaticamente serão sequenciais a partir de onde a sequence estava
    -- Em um ambiente de teste limpo, Id 1 e 2 são prováveis
END $$;


-- Inserir dados na tabela PedidoXProduto (Itens do Pedido)
DO $$
DECLARE
    pedido1_id INT := (SELECT Id FROM Pedido WHERE Comentarios = 'Tocar campainha forte' LIMIT 1); -- Busca pelo comentário para encontrar o Pedido 1
    pedido2_id INT := (SELECT Id FROM Pedido WHERE Comentarios IS NULL LIMIT 1); -- Busca por comentário NULL para encontrar o Pedido 2 (menos robusto, apenas para exemplo)
    pizza_calabresa_id INT := (SELECT Id FROM Produto WHERE Nome = 'Pizza Calabresa');
    duplo_cheeseburger_id INT := (SELECT Id FROM Produto WHERE Nome = 'Duplo Cheeseburger');
    refri_lata_id INT := (SELECT Id FROM Produto WHERE Nome = 'Refrigerante Lata');
    petit_gateau_id INT := (SELECT Id FROM Produto WHERE Nome = 'Petit Gateau');
BEGIN
    -- Itens do Pedido 1 (João)
    INSERT INTO PedidoXProduto (PedidoId, ProdutoId, Quantidade, Comentario) VALUES
    (pedido1_id, pizza_calabresa_id, 1, 'Sem cebola'), -- Pizza Calabresa sem cebola
    (pedido1_id, refri_lata_id, 2, NULL); -- 2 Refrigerantes Lata

    -- Itens do Pedido 2 (Maria)
    INSERT INTO PedidoXProduto (PedidoId, ProdutoId, Quantidade, Comentario) VALUES
    (pedido2_id, duplo_cheeseburger_id, 1, NULL), -- Duplo Cheeseburger
    (pedido2_id, petit_gateau_id, 1, NULL); -- Petit Gateau

END $$;


-- Inserir dados nas tabelas associativas de Adicionais e Acompanhamentos aos Itens de Pedido
DO $$
DECLARE
    pizza_pedido1_prod_pedido_id INT := (SELECT PXP.Id FROM PedidoXProduto PXP JOIN Pedido P ON PXP.PedidoId = P.Id WHERE P.Comentarios = 'Tocar campainha forte' AND PXP.Comentario = 'Sem cebola' LIMIT 1); -- ID do item Pizza Calabresa no Pedido 1
    duplo_cheeseburger_pedido2_prod_pedido_id INT := (SELECT PXP.Id FROM PedidoXProduto PXP JOIN Pedido P ON PXP.PedidoId = P.Id WHERE P.Comentarios IS NULL AND PXP.ProdutoId = (SELECT Id FROM Produto WHERE Nome = 'Duplo Cheeseburger') LIMIT 1); -- ID do item Duplo Cheeseburger no Pedido 2

    bacon_adicional_id INT := (SELECT Id FROM Adicional WHERE Nome = 'Bacon Extra');
    cheddar_adicional_id INT := (SELECT Id FROM Adicional WHERE Nome = 'Cheddar Cremoso');
    batata_pequena_acompanhamento_id INT := (SELECT Id FROM Acompanhamento WHERE Nome = 'Batata Frita Pequena');
    ketchup_acompanhamento_id INT := (SELECT Id FROM Acompanhamento WHERE Nome = 'Ketchup');
    maionese_acompanhamento_id INT := (SELECT Id FROM Acompanhamento WHERE Nome = 'Maionese');
BEGIN
    -- Adicionais para a Pizza Calabresa no Pedido 1
    INSERT INTO AdicionalProdutoPedido (ProdutoPedidoId, AdicionalId) VALUES
    (pizza_pedido1_prod_pedido_id, (SELECT Id FROM Adicional WHERE Nome = 'Borda Recheada Catupiry'));

    -- Acompanhamentos para o Duplo Cheeseburger no Pedido 2
    INSERT INTO AcompanhamentoProdutoPedido (ProdutoPedidoId, AcompanhamentoId) VALUES
    (duplo_cheeseburger_pedido2_prod_pedido_id, batata_pequena_acompanhamento_id), -- 1 Batata Frita Pequena (assumindo que está dentro do limite grátis)
    (duplo_cheeseburger_pedido2_prod_pedido_id, ketchup_acompanhamento_id),      -- Ketchup (grátis)
    (duplo_cheeseburger_pedido2_prod_pedido_id, maionese_acompanhamento_id);     -- Maionese (grátis)

END $$;


-- Inserir dados na tabela CupomProduto (Quais produtos um cupom se aplica)
INSERT INTO CupomProduto (CodigoCupom, ProdutoId) VALUES
('PRIMEIRACOMPRA15', (SELECT Id FROM Produto WHERE Nome = 'Duplo Cheeseburger')), -- Cupom se aplica ao Duplo Cheeseburger
('MAIS5OFF', (SELECT Id FROM Produto WHERE Nome = 'Refrigerante Lata')); -- Cupom se aplica ao Refrigerante Lata
-- O cupom FRETEZERO geralmente não se aplica a produtos específicos, mas ao pedido inteiro.
-- Se ele precisasse estar aqui para alguma regra, a lógica seria diferente.