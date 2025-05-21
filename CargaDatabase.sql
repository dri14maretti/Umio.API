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

INSERT INTO CategoriaProduto (Categoria) VALUES
('Massas'),
('Saladas'),
('Lanchinhos'),
('Pastéis'),
('Porções'),
('Burgers 160g'),
('Vegetarianos'),
('Smash Frango'),
('Smash Burgers'),
('Bebidas');

-- Inserindo Produtos na tabela Produto
-- Assumindo que os Ids da CategoriaProduto são gerados sequencialmente a partir de 1

-- Massas (CategoriaId = 1)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Umió Mac''n Cheese', 31.90, 'Penne envolvido em um cremoso molho cheddar, acompanhado de pedacinhos de bacon para um toque irresistível de sabor.', 1, NULL),
('Spaghetti na Manteiga', 31.90, 'Spaghetti solteado na manteiga, combinado com alho dourado, brócolis fresquinho, bacon e finalizado com uma chuva de parmesão derretido.', 1, NULL),
('Penne ao Molho Gorgonzola', 31.90, 'Penne envolvido em um aveludado molho gorgonzola, finalizado com folhas frescas de manjericão para um aroma especial e um sabor marcante.', 1, NULL),
('Spaghetti alla Napoletana', 31.90, 'Spaghetti envolvido em um suculento molho pomodoro caseiro, coberto com pedacinhos de bacon, uma generosa camada de parmesão ralado.', 1, NULL);

-- Saladas (CategoriaId = 2)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Umió Caesar', 27.90, 'Uma deliciosa salada de alface americana, tomatinho cereja, queijo parmesão ralado e frango grelhado, acompanhada de molho Caesar especial.', 2, NULL);

-- Lanchinhos (CategoriaId = 3)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Umió Misto Especial', 12.90, 'Nosso tradicional pão brioche fofinho, tostado e recheado com presunto, queijo mussarela, Catupiry Original e alface.', 3, NULL),
('Frango Natural', 15.90, 'Nosso tradicional pão brioche fofinho, recheado de frango desfiado, Catupiry Original, alface, tomate, cebola roxa, batata palha.', 3, NULL);

-- Pastéis (CategoriaId = 4)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Pastel 14x10cm', 9.90, 'Um delicioso pastel frito de massa 100% artesanal, acompanhado de um molho especial a sua escolha!', 4, NULL),
('Pastel 14x21cm', 15.90, 'Um delicioso pastel frito de massa 100% artesanal, acompanhado de um molho especial a sua escolha!', 4, NULL);

-- Porções (CategoriaId = 5)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Strogonoff de Frango', 29.90, 'Uma generosa porção do nosso irresistível strogonoff de frango, cremoso e cheio de sabor, acompanhada de batata palha crocante para completar.', 5, NULL),
('Calabresa Acebolada', 22.90, 'Uma porção generosa de calabresa fatiada e grelhada na chapa, soltando aquele aroma irresistível, acompanhada de cebola douradinha.', 5, NULL),
('Batata Rústica', 22.90, 'Uma porção deliciosa de batatas rústicas, crocantes por fora e macias por dentro, acompanhadas do molho de sua escolha: barbecue, cheddar ou maionese especial.', 5, NULL),
('Onion Rings', 22.90, 'Uma porção de oito onion rings crocantes e douradinhas, fritinhas na medida certa, acompanhadas dos nossos molhos especiais para dar aquele toque final.', 5, NULL);

-- Burgers 160g (CategoriaId = 6)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Do Meu Jeito é Mió (Burger 160g)', 21.90, 'Pão brioche, hambúrguer artesanal 160g. Agora é só montar do jeito que quiser!', 6, NULL),
('Burger 160g', 25.90, 'Pão brioche, hambúrguer artesanal 160g, cheddar fatiado.', 6, NULL),
('Salada 160g', 28.90, 'Pão brioche, hambúrguer artesanal 160g, cheddar fatiado, alface, tomate, cebola roxa e maionese especial.', 6, NULL),
('Catupiry 160g', 32.90, 'Pão brioche, hambúrguer artesanal 160g, Catupiry (De Verdade) e cebola caramelizada.', 6, NULL),
('Bacon 160g', 33.90, 'Pão brioche, hambúrguer artesanal 160g, american cheese, bacon, tomate e maionese especial.', 6, NULL),
('Barbecue 160g', 33.90, 'Pão brioche, hambúrguer artesanal 160g, cheddar fatiado, bacon e molho barbecue.', 6, NULL),
('Cheddar 160g', 35.90, 'Pão brioche, hambúrguer artesanal 160g, cheddar cremoso, bacon e molho cheddar.', 6, NULL),
('Lanche Misterioso', 36.90, 'Confia no cozinheiro! Aqui é surpresa: você não escolhe os ingredientes. Só diga o que não gosta ou não pode comer. Quer montar tudo do seu jeito? Peça o ''Do Meu Jeito é Mió''.', 6, NULL),
('Onion 160g', 38.90, 'Pão brioche, hambúrguer artesanal 160g, Catupiry (De Verdade), onion rings, bacon e cebola caramelizada.', 6, NULL),
('Tower', 39.90, 'Pão brioche, 2 hambúrgueres de 160g, 2 vezes queijo cheddar fatiado, bacon e molho barbecue ou cheddar (você escolhe).', 6, NULL);

-- Vegetarianos (CategoriaId = 7)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Do Meu Jeito é Mió (Incrível Veg)', 18.90, 'Pão brioche, incrível burger 100% vegetal (110g). Agora é só montar do jeito que quiser!', 7, NULL),
('Incrível Veg', 31.90, 'Pão brioche, incrível burger 100% vegetal (110g), cheddar fatiado, ovo, alface, tomate, cebola roxa e maionese especial.', 7, NULL),
('Gold', 32.90, 'Pão brioche, incrível burger 100% vegetal (110g), cheddar cremoso, cebola caramelizada, onion rings crocantes e molho barbecue.', 7, NULL),
('Fantasy', 32.90, 'Pão brioche, incrível burger 100% vegetal (110g), Catupiry (De Verdade), onion rings crocantes e cebola caramelizada.', 7, NULL),
('Quaresma', 20.90, 'Pão brioche, american cheese, ovo, alface, tomate, cebola roxa, batata palha e maionese especial.', 7, NULL);

-- Smash Frango (CategoriaId = 8)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Do Meu Jeito é Mió (Frango)', 11.90, 'Pão brioche, hambúrguer smash 80g de frango. Agora é só montar do jeito que quiser!', 8, NULL),
('Frango Smash', 21.90, 'Pão brioche, hambúrguer smash 80g de frango, cheddar fatiado, batata palha e relish de cebola roxa artesanal.', 8, NULL),
('Frango Smash Bacon', 25.90, 'Pão brioche, hambúrguer smash 80g de frango, cheddar fatiado, bacon, tomate, batata palha e relish de cebola roxa artesanal.', 8, NULL),
('Frango Smash Tudo', 28.90, 'Pão brioche, hambúrguer smash 80g de frango, cheddar fatiado, bacon, calabresa, ovo, alface, tomate, relish de cebola roxa artesanal e batata palha.', 8, NULL);

-- Smash Burgers (CategoriaId = 9)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Do Meu Jeito é Mió (Smash)', 14.90, 'Pão brioche, hambúrguer smash 80g. Agora é só montar do jeito que quiser!', 9, NULL),
('Smash Double Burger', 25.90, 'Pão brioche, dois hambúrgueres smash 80g, cheddar fatiado e batata palha.', 9, NULL),
('Smash Bacon', 29.90, 'Pão brioche, hambúrguer smash 80g, cheddar fatiado, bacon, tomate e batata palha.', 9, NULL),
('Smash Tudo', 32.90, 'Pão brioche, hambúrguer smash 80g, cheddar fatiado, bacon, calabresa, ovo, alface, tomate e batata palha.', 9, NULL);

-- Bebidas (CategoriaId = 10)
INSERT INTO Produto (Nome, Preco, Descricao, CategoriaId, Imagem) VALUES
('Coca Cola', 5.90, 'Lata de Coca Cola de 350ml', 10, NULL),
('Coca Cola Zero', 5.90, 'Lata de Coca Cola Zero de 350ml', 10, NULL),
('Guaraná Antarctica', 5.90, 'Lata de Guaraná Antarctica de 350ml', 10, NULL),
('Guaraná Antarctica Zero', 5.90, 'Lata de Guaraná Antarctica Zero de 350ml', 10, NULL);

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
('PRIMEIRACOMPRA15', (SELECT Id FROM Produto WHERE Nome = 'Burger 160g')), -- Cupom se aplica ao Duplo Cheeseburger
('MAIS5OFF', (SELECT Id FROM Produto WHERE Nome = 'Coca Cola Zero')); -- Cupom se aplica ao Refrigerante Lata
-- O cupom FRETEZERO geralmente não se aplica a produtos específicos, mas ao pedido inteiro.
-- Se ele precisasse estar aqui para alguma regra, a lógica seria diferente.