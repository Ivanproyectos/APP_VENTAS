CREATE PROCEDURE [dbo].[USP_INVEN_PRODUCTO_ACTUALIZAR](
  @PI_ID_PRODUCTO        INT,
  @PI_COD_PRODUCTO   VARCHAR(200),
  @PI_DESC_PRODUCTO   VARCHAR(200),
  @PI_ID_SUCURSAL    INT,
  @PI_ID_UNIDAD_MEDIDA   INT,
  @PI_PRECIO_COMPRA    DECIMAL(18,2),
  @PI_PRECIO_VENTA   DECIMAL(18,2),
  @PI_STOCK    INT,
  @PI_STOCK_MINIMO    DECIMAL(18,2),
  @PI_FLG_SERIVICIO    INT,
  @PI_FLG_VENCE    INT,
  @PI_FECHA_VENCIMIENTO    VARCHAR(200),
  @PI_MARCA    VARCHAR(200),
  @PI_MODELO    VARCHAR(200),
  @PI_DETALLE    VARCHAR(1000),
  @PI_USUARIO_MODIFICACION VARCHAR(200),
  @PO_VALIDO           INT OUT ,  -- 0 : HAY ERROR , 1 : NO HAY ERROR
  @PO_MENSAJE          VARCHAR(200) OUT  -- MENSAJE DEL ERROR
)
AS
--P_CUENTA  INT:= 0;
DECLARE @P_MENSAJE VARCHAR(200)= '';
BEGIN
    SET @P_MENSAJE =(  
	      SELECT   
			ISNULL((
            SELECT
			 TOP 1 
               CASE
                  WHEN UPPER(U.DESC_PRODUCTO) = UPPER(RTRIM(@PI_DESC_PRODUCTO)) THEN
                      'La descripción del producto o servicio ya existe'
                WHEN UPPER(U.COD_PRODUCTO) = UPPER(RTRIM(@PI_COD_PRODUCTO)) THEN
                'La descripción del producto o servicio ya existe'
                 END 
            FROM
                T_M_PRODUCTO U
            WHERE
                (
                 UPPER(U.DESC_PRODUCTO) = UPPER(RTRIM(@PI_DESC_PRODUCTO ))	
                 OR UPPER(U.COD_PRODUCTO) = UPPER(RTRIM(@PI_COD_PRODUCTO ))	
                )
				 AND U.ID_PRODUCTO != @PI_ID_PRODUCTO

            ),'-') ); 

        IF @P_MENSAJE = '-' 
		 BEGIN
            UPDATE
               T_M_PRODUCTO
            SET
                DESC_PRODUCTO = RTRIM(@PI_DESC_PRODUCTO),
                COD_PRODUCTO = RTRIM(@PI_COD_PRODUCTO),
                ID_SUCURSAL = @PI_ID_SUCURSAL,
                ID_UNIDAD_MEDIDA = @PI_ID_UNIDAD_MEDIDA,
                PRECIO_COMPRA = @PI_PRECIO_COMPRA ,
                PRECIO_VENTA = @PI_PRECIO_VENTA ,
                STOCK = @PI_STOCK ,
                STOCK_MINIMO = @PI_STOCK_MINIMO ,
                FLG_SERIVICIO = @PI_FLG_SERIVICIO,
                FLG_VENCE =  @PI_FLG_VENCE,
                FECHA_VENCIMIENTO = @PI_FECHA_VENCIMIENTO,
                MARCA = RTRIM(@PI_MARCA),
                MODELO = RTRIM(@PI_MODELO),
                DETALLE = RTRIM(@PI_DETALLE),
                USU_MODIFICACION = @PI_USUARIO_MODIFICACION,
                FEC_MODIFICACION = GETDATE()
            WHERE
                ID_PRODUCTO = @PI_ID_PRODUCTO;

           SET @PO_VALIDO  = 1;
		   END 
        ELSE
		 BEGIN
            SET @PO_VALIDO  = 0;
	    END
    SET @PO_MENSAJE = @P_MENSAJE;
END;




go




CREATE PROCEDURE [dbo].[USP_INVEN_PRODUCTO_ELIMINAR](
  @PI_ID_PRODUCTO         INT,
  @PO_VALIDO            INT OUT   -- 0 : HAY ERROR , 1 : NO HAY ERROR
)
AS
BEGIN
    DELETE FROM T_M_PRODUCTO
    WHERE ID_PRODUCTO = @PI_ID_PRODUCTO;
    SET @PO_VALIDO =1;
END;




go


CREATE PROCEDURE [dbo].[USP_INVEN_PRODUCTO_ESTADO](
  @PI_ID_PRODUCTO         INT,
  @PI_FLG_ESTADO        CHAR(1),
  @PI_IP_MODIFICACION   VARCHAR(200),
  @PI_USUARIO_MODIFICACION VARCHAR(200),
  @PO_VALIDO            INT OUT   -- 0 : HAY ERROR , 1 : NO HAY ERROR
)
AS
BEGIN
    UPDATE T_M_PRODUCTO SET
           FLG_ESTADO = @PI_FLG_ESTADO,
           USU_MODIFICACION= @PI_USUARIO_MODIFICACION,
           FEC_MODIFICACION=GETDATE()
    WHERE ID_PRODUCTO = @PI_ID_PRODUCTO;
    SET @PO_VALIDO =1;
END;




go




 CREATE PROCEDURE [dbo].[USP_INVEN_PRODUCTO_INSERTAR](
  @PI_DESC_PRODUCTO   VARCHAR(200),
  @PI_DESCRIPCION    VARCHAR(1000),
  @PI_USUARIO_CREACION  VARCHAR(200),
  @PO_VALIDO            INT OUT ,  -- 0 : HAY ERROR , 1 : NO HAY ERROR
  @PO_MENSAJE           VARCHAR(200) OUT  -- MENSAJE DEL ERROR
)
AS
DECLARE @P_MENSAJE   VARCHAR(200)= ''; 
BEGIN
   SET @P_MENSAJE = '';


	 SET @P_MENSAJE = (
        SELECT   
			ISNULL((
            SELECT
			 TOP 1 
               CASE
                  WHEN UPPER(U.DESC_PRODUCTO ) = UPPER(RTRIM(@PI_DESC_PRODUCTO )) THEN
                      'La descripción ya existe'
                 END 
            FROM
                T_M_PRODUCTO U
            WHERE
                (
                 UPPER(U.DESC_PRODUCTO ) = UPPER(RTRIM(@PI_DESC_PRODUCTO ))
                )
            ),'-') ); 


        IF @P_MENSAJE = '-' 
		  BEGIN 
            INSERT INTO
               T_M_PRODUCTO
                (
					DESC_PRODUCTO,
					DESCRIPCION,

					USU_CREACION,
					FEC_CREACION
                )
                VALUES
                (
				  @PI_DESC_PRODUCTO,
                  @PI_DESCRIPCION, 
                  @PI_USUARIO_CREACION,
                  GETDATE()
                );

            SET @PO_VALIDO = 1;
			END 
        ELSE
		BEGIN 
            SET @PO_VALIDO  = 0;
        END 
   SET  @PO_MENSAJE = @P_MENSAJE;

END;





go


CREATE PROCEDURE [dbo].[USP_INVEN_PRODUCTO_LISTAR](
  @PI_DESC_PRODUCTO VARCHAR(200),
  @PI_FLG_ESTADO INT
)
AS
BEGIN
        SELECT
			V.ID_PRODUCTO,
			V.DESC_PRODUCTO,
			V.DESCRIPCION,
			V.USU_CREACION,
			V.FLG_ESTADO,
			ISNULL(CONCAT(CONVERT(VARCHAR,V.FEC_CREACION, 103), ' ', CONVERT(VARCHAR, V.FEC_CREACION, 8)), '-')FECHA_CREACION,
			V.USU_MODIFICACION,
			ISNULL(CONCAT(CONVERT(VARCHAR,V.FEC_MODIFICACION, 103), ' ', CONVERT(VARCHAR, V.FEC_MODIFICACION, 8)), '-')FECHA_MODIFICACION
        FROM
            T_M_PRODUCTO V

        WHERE
             UPPER(V.DESC_PRODUCTO)   LIKE '%' +ISNULL(@PI_DESC_PRODUCTO,V.DESC_PRODUCTO) +'%'
            AND  UPPER(V.FLG_ESTADO)  = ISNULL(@PI_FLG_ESTADO,V.FLG_ESTADO) 
        ORDER BY V.ID_PRODUCTO DESC;
END;


go



 
 CREATE PROCEDURE [dbo].[USP_INVEN_PRODUCTO_LISTAR_UNO](
  @PI_ID_PRODUCTO INT
)
AS
BEGIN
        SELECT
			V.ID_PRODUCTO,
			V.DESC_PRODUCTO,
			V.DESCRIPCION
        FROM
            T_M_PRODUCTO V
        WHERE
             V.ID_PRODUCTO  = @PI_ID_PRODUCTO
END;
 