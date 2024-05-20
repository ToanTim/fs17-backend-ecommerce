using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class create_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:order_status", "created,processing,completed,cancelled")
                .Annotation("Npgsql:Enum:sort_by", "price,rating,popularity,date")
                .Annotation("Npgsql:Enum:sort_order", "desc,asc")
                .Annotation("Npgsql:Enum:user_role", "super_admin,admin,user")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    image = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    last_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    inventory = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_line = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    country = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_addresses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_anonymous = table.Column<bool>(type: "boolean", nullable: false),
                    content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: true),
                    review_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_images_reviews_review_id",
                        column: x => x.review_id,
                        principalTable: "reviews",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: true),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    order_id1 = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id1",
                        column: x => x.order_id1,
                        principalTable: "orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_order_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_at", "image", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000101"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3817), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Electronics", null },
                    { new Guid("00000000-0000-0000-0000-000000000102"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3909), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Clothing", null },
                    { new Guid("00000000-0000-0000-0000-000000000103"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3918), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Books", null },
                    { new Guid("00000000-0000-0000-0000-000000000104"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3926), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Home & Garden", null },
                    { new Guid("00000000-0000-0000-0000-000000000105"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3935), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Sports & Outdoors", null },
                    { new Guid("00000000-0000-0000-0000-000000000106"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3944), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Toys & Games", null },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3953), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Beauty & Personal Care", null },
                    { new Guid("00000000-0000-0000-0000-000000000108"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3961), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Automotive", null },
                    { new Guid("00000000-0000-0000-0000-000000000109"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(3971), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Health & Household", null },
                    { new Guid("00000000-0000-0000-0000-000000000110"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(4047), new TimeSpan(0, 3, 0, 0, 0)), "https://us.123rf.com/450wm/123rfexclusive/123rfexclusive2206/123rfexclusive220600768/188034022-3d-shop-sale-concept.jpg?ver=6", "Food & Grocery", null }
                });

            migrationBuilder.InsertData(
                table: "images",
                columns: new[] { "id", "created_at", "entity_id", "product_id", "review_id", "url" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000601"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4245), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000301"), null, null, "http://example.com/product_image_smartphone1.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000602"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4302), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000301"), null, null, "http://example.com/product_image_smartphone2.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000603"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4311), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000302"), null, null, "http://example.com/product_image_men_tshirt1.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000604"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4318), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000302"), null, null, "http://example.com/product_image_men_tshirt2.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000605"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4325), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000302"), null, null, "http://example.com/product_image_men_tshirt3.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000606"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4341), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000303"), null, null, "http://example.com/product_image_programming_book.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000607"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4348), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000304"), null, null, "http://example.com/product_image_garden_hose.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000608"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4355), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000305"), null, null, "http://example.com/product_image_running_shoes.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000609"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4362), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000306"), null, null, "http://example.com/product_image_board_game.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000610"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4391), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000307"), null, null, "http://example.com/product_image_shampoo.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000611"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4399), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000308"), null, null, "http://example.com/product_image_car_battery.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000612"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4409), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000309"), null, null, "http://example.com/product_image_vitamin_supplement.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000613"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4426), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000310"), null, null, "http://example.com/product_image_cereal.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000614"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4437), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000311"), null, null, "http://example.com/product_image_laptop.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000615"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4448), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000312"), null, null, "http://example.com/product_image_womens_dress.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000616"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4459), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000313"), null, null, "http://example.com/product_image_ebook_reader.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000617"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000314"), null, null, "http://example.com/product_image_lawn_mower.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000618"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4481), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000315"), null, null, "http://example.com/product_image_fitness_tracker.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000619"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4525), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000316"), null, null, "http://example.com/product_image_puzzle_game.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000620"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4540), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000317"), null, null, "http://example.com/product_image_hair_conditioner.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000621"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4551), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000318"), null, null, "http://example.com/product_image_car_wax.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000622"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4562), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000319"), null, null, "http://example.com/product_image_protein_powder.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000623"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4573), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000320"), null, null, "http://example.com/product_image_organic_juice.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000624"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4583), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000501"), null, null, "http://example.com/review_image_1_1.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000625"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4640), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000501"), null, null, "http://example.com/review_image_1_2.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000626"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4649), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000502"), null, null, "http://example.com/review_image_2_1.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000627"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4656), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000502"), null, null, "http://example.com/review_image_2_2.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000628"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4688), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000502"), null, null, "http://example.com/review_image_2_3.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000629"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4696), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000503"), null, null, "http://example.com/review_image_3.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000630"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4703), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000504"), null, null, "http://example.com/review_image_4.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000631"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4710), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000505"), null, null, "http://example.com/review_image_5.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000632"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4723), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000506"), null, null, "http://example.com/review_image_6.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000633"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4730), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000507"), null, null, "http://example.com/review_image_7.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000634"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4739), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000508"), null, null, "http://example.com/review_image_8.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000635"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4746), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000509"), null, null, "http://example.com/review_image_9.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000636"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4774), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000510"), null, null, "http://example.com/review_image_10.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000637"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4781), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000511"), null, null, "http://example.com/review_image_11.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000638"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4787), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000512"), null, null, "http://example.com/review_image_12.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000639"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4799), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000513"), null, null, "http://example.com/review_image_13.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000640"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4805), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000514"), null, null, "http://example.com/review_image_14.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000641"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4812), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000515"), null, null, "http://example.com/review_image_15.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000642"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4818), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000516"), null, null, "http://example.com/review_image_16.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000643"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4825), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000517"), null, null, "http://example.com/review_image_17.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000644"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4831), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000518"), null, null, "http://example.com/review_image_18.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000645"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4908), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000519"), null, null, "http://example.com/review_image_19.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000646"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(4937), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000520"), null, null, "http://example.com/review_image_20.jpg" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "created_at", "email", "first_name", "last_name", "password", "role", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "http://example.com/avatar1.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5032), new TimeSpan(0, 3, 0, 0, 0)), "john@example.com", "John", "Doe", "password1", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "http://example.com/avatar2.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5199), new TimeSpan(0, 3, 0, 0, 0)), "jane@example.com", "Jane", "Smith", "password2", "admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "http://example.com/avatar3.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5219), new TimeSpan(0, 3, 0, 0, 0)), "alice@example.com", "Alice", "Johnson", "password3", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "http://example.com/avatar4.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5232), new TimeSpan(0, 3, 0, 0, 0)), "michael@example.com", "Michael", "Brown", "password4", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "http://example.com/avatar5.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5243), new TimeSpan(0, 3, 0, 0, 0)), "emily@example.com", "Emily", "Williams", "password5", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "http://example.com/avatar6.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5257), new TimeSpan(0, 3, 0, 0, 0)), "david@example.com", "David", "Jones", "password6", "admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "http://example.com/avatar7.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5284), new TimeSpan(0, 3, 0, 0, 0)), "samantha@example.com", "Samantha", "Miller", "password7", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "http://example.com/avatar8.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5298), new TimeSpan(0, 3, 0, 0, 0)), "chris@example.com", "Christopher", "Taylor", "password8", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "http://example.com/avatar9.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5341), new TimeSpan(0, 3, 0, 0, 0)), "elizabeth@example.com", "Elizabeth", "Wilson", "password9", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "http://example.com/avatar10.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5355), new TimeSpan(0, 3, 0, 0, 0)), "james@example.com", "James", "Anderson", "password10", "admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "http://example.com/avatar11.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5368), new TimeSpan(0, 3, 0, 0, 0)), "olivia@example.com", "Olivia", "Moore", "password11", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "http://example.com/avatar12.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5380), new TimeSpan(0, 3, 0, 0, 0)), "ethan@example.com", "Ethan", "Martinez", "password12", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "http://example.com/avatar13.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5400), new TimeSpan(0, 3, 0, 0, 0)), "sophia@example.com", "Sophia", "Garcia", "password13", "admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "http://example.com/avatar14.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5413), new TimeSpan(0, 3, 0, 0, 0)), "daniel@example.com", "Daniel", "Clark", "password14", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "http://example.com/avatar15.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5426), new TimeSpan(0, 3, 0, 0, 0)), "isabella@example.com", "Isabella", "Rodriguez", "password15", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "http://example.com/avatar16.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5463), new TimeSpan(0, 3, 0, 0, 0)), "henry@example.com", "Henry", "Lewis", "password16", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "http://example.com/avatar17.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5477), new TimeSpan(0, 3, 0, 0, 0)), "charlotte@example.com", "Charlotte", "Lee", "password17", "admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "http://example.com/avatar18.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5490), new TimeSpan(0, 3, 0, 0, 0)), "benjamin@example.com", "Benjamin", "Walker", "password18", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "http://example.com/avatar19.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5548), new TimeSpan(0, 3, 0, 0, 0)), "amelia@example.com", "Amelia", "Hall", "password19", "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "http://example.com/avatar20.jpg", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(5562), new TimeSpan(0, 3, 0, 0, 0)), "lucas@example.com", "Lucas", "Allen", "password20", "user", null }
                });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "address_line", "country", "created_at", "phone_number", "postal_code", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000401"), "123 Main St", "USA", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8423), new TimeSpan(0, 3, 0, 0, 0)), "+1-555-123-4567", "12345", null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000402"), "456 Elm St", "Canada", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8462), new TimeSpan(0, 3, 0, 0, 0)), "+1-555-987-6543", "67890", null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000403"), "789 Oak St", "UK", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8475), new TimeSpan(0, 3, 0, 0, 0)), "+44-20-1234-5678", "54321", null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000404"), "101 Pine St", "Australia", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8487), new TimeSpan(0, 3, 0, 0, 0)), "+61-2-8765-4321", "98765", null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000405"), "202 Maple St", "Germany", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8497), new TimeSpan(0, 3, 0, 0, 0)), "+49-30-2468-1357", "13579", null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000406"), "303 Cedar St", "France", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8510), new TimeSpan(0, 3, 0, 0, 0)), "+33-1-3579-2468", "24680", null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000407"), "404 Birch St", "Spain", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8534), new TimeSpan(0, 3, 0, 0, 0)), "+34-91-753-8692", "97531", null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000408"), "505 Walnut St", "Italy", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8577), new TimeSpan(0, 3, 0, 0, 0)), "+39-02-9753-8642", "86420", null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000409"), "606 Fir St", "Japan", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8589), new TimeSpan(0, 3, 0, 0, 0)), "+81-3-8642-9753", "12312", null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000410"), "707 Pineapple St", "Brazil", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8600), new TimeSpan(0, 3, 0, 0, 0)), "+55-11-9753-8642", "78901", null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000411"), "808 Palm St", "Mexico", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8700), new TimeSpan(0, 3, 0, 0, 0)), "+52-55-1234-5678", "98765", null, new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0000-000000000412"), "909 Magnolia St", "China", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8718), new TimeSpan(0, 3, 0, 0, 0)), "+86-10-8765-4321", "65432", null, new Guid("00000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-0000-0000-0000-000000000413"), "1010 Cedar St", "Russia", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8740), new TimeSpan(0, 3, 0, 0, 0)), "+7-495-8765-4321", "43210", null, new Guid("00000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-0000-0000-0000-000000000414"), "1111 Maple Ave", "New Zealand", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8752), new TimeSpan(0, 3, 0, 0, 0)), "+64-9-8765-4321", "87654", null, new Guid("00000000-0000-0000-0000-000000000014") },
                    { new Guid("00000000-0000-0000-0000-000000000415"), "1212 Cherry St", "India", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8763), new TimeSpan(0, 3, 0, 0, 0)), "+91-11-1234-5678", "56789", null, new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-000000000416"), "1313 Cedar St", "South Africa", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8807), new TimeSpan(0, 3, 0, 0, 0)), "+27-21-8765-4321", "24689", null, new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-000000000417"), "1414 Pine St", "Turkey", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8820), new TimeSpan(0, 3, 0, 0, 0)), "+90-212-1234-5678", "97531", null, new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-000000000418"), "1515 Elm Ave", "Netherlands", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8832), new TimeSpan(0, 3, 0, 0, 0)), "+31-20-8765-4321", "12312", null, new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-000000000419"), "1616 Walnut St", "Singapore", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8899), new TimeSpan(0, 3, 0, 0, 0)), "+65-3-4321-5678", "65432", null, new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000420"), "1717 Fir St", "Sweden", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(8938), new TimeSpan(0, 3, 0, 0, 0)), "+46-8-8765-4321", "78901", null, new Guid("00000000-0000-0000-0000-000000000020") }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "created_at", "description", "inventory", "name", "price", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000301"), new Guid("00000000-0000-0000-0000-000000000101"), new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "High-performance smartphone with latest features", 10, "Smartphone", 599.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000302"), new Guid("00000000-0000-0000-0000-000000000102"), new DateTimeOffset(new DateTime(2024, 4, 23, 8, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Comfortable cotton t-shirt for men", 11, "Men T-Shirt", 29.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000303"), new Guid("00000000-0000-0000-0000-000000000103"), new DateTimeOffset(new DateTime(2024, 4, 23, 8, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Comprehensive guide to programming languages", 12, "Programming Book", 49.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000304"), new Guid("00000000-0000-0000-0000-000000000104"), new DateTimeOffset(new DateTime(2024, 4, 23, 8, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Durable garden hose for watering plants", 13, "Garden Hose", 39.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000305"), new Guid("00000000-0000-0000-0000-000000000105"), new DateTimeOffset(new DateTime(2024, 4, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Lightweight running shoes for outdoor activities", 14, "Running Shoes", 79.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000306"), new Guid("00000000-0000-0000-0000-000000000106"), new DateTimeOffset(new DateTime(2024, 4, 23, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Fun board game for family entertainment", 15, "Board Game", 24.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000307"), new Guid("00000000-0000-0000-0000-000000000107"), new DateTimeOffset(new DateTime(2024, 4, 23, 9, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Moisturizing shampoo for healthy hair", 16, "Shampoo", 9.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000308"), new Guid("00000000-0000-0000-0000-000000000108"), new DateTimeOffset(new DateTime(2024, 4, 23, 9, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "High-quality battery for automotive use", 17, "Car Battery", 129.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000309"), new Guid("00000000-0000-0000-0000-000000000109"), new DateTimeOffset(new DateTime(2024, 4, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Essential vitamins for overall health", 18, "Vitamin Supplement", 19.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000310"), new Guid("00000000-0000-0000-0000-000000000110"), new DateTimeOffset(new DateTime(2024, 4, 23, 10, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Nutritious breakfast cereal for a balanced diet", 19, "Cereal", 3.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000311"), new Guid("00000000-0000-0000-0000-000000000101"), new DateTimeOffset(new DateTime(2024, 4, 23, 10, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "High-performance laptop with advanced features", 20, "Laptop", 999.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000312"), new Guid("00000000-0000-0000-0000-000000000102"), new DateTimeOffset(new DateTime(2024, 4, 23, 10, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Stylish dress suitable for all occasions", 21, "Women's Dress", 59.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000313"), new Guid("00000000-0000-0000-0000-000000000103"), new DateTimeOffset(new DateTime(2024, 4, 23, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Portable e-book reader with a large display", 22, "E-book Reader", 149.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000314"), new Guid("00000000-0000-0000-0000-000000000104"), new DateTimeOffset(new DateTime(2024, 4, 23, 11, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Efficient electric lawn mower for your garden", 23, "Lawn Mower", 199.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000315"), new Guid("00000000-0000-0000-0000-000000000105"), new DateTimeOffset(new DateTime(2024, 4, 23, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Wearable fitness tracker for active lifestyle", 24, "Fitness Tracker", 79.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000316"), new Guid("00000000-0000-0000-0000-000000000106"), new DateTimeOffset(new DateTime(2024, 4, 23, 11, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Intricate puzzle game for mental challenges", 25, "Puzzle Game", 14.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000317"), new Guid("00000000-0000-0000-0000-000000000107"), new DateTimeOffset(new DateTime(2024, 4, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Nourishing conditioner for smooth hair", 26, "Hair Conditioner", 12.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000318"), new Guid("00000000-0000-0000-0000-000000000108"), new DateTimeOffset(new DateTime(2024, 4, 23, 12, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "High-gloss car wax for a shiny finish", 27, "Car Wax", 39.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000319"), new Guid("00000000-0000-0000-0000-000000000109"), new DateTimeOffset(new DateTime(2024, 4, 23, 12, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Protein supplement for muscle growth", 28, "Protein Powder", 29.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("00000000-0000-0000-0000-000000000320"), new Guid("00000000-0000-0000-0000-000000000110"), new DateTimeOffset(new DateTime(2024, 4, 23, 12, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Refreshing organic juice for a healthy diet", 29, "Organic Juice", 4.99m, new DateTimeOffset(new DateTime(2024, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "id", "address_id", "created_at", "status", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000601"), new Guid("00000000-0000-0000-0000-000000000401"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9648), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000602"), new Guid("00000000-0000-0000-0000-000000000401"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9801), new TimeSpan(0, 3, 0, 0, 0)), "Completed", null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000603"), new Guid("00000000-0000-0000-0000-000000000402"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9818), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000604"), new Guid("00000000-0000-0000-0000-000000000402"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9826), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000605"), new Guid("00000000-0000-0000-0000-000000000402"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9834), new TimeSpan(0, 3, 0, 0, 0)), "Completed", null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000606"), new Guid("00000000-0000-0000-0000-000000000403"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9844), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000607"), new Guid("00000000-0000-0000-0000-000000000403"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9862), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000608"), new Guid("00000000-0000-0000-0000-000000000403"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9870), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000609"), new Guid("00000000-0000-0000-0000-000000000404"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9878), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000610"), new Guid("00000000-0000-0000-0000-000000000404"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9928), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000611"), new Guid("00000000-0000-0000-0000-000000000404"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9937), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000612"), new Guid("00000000-0000-0000-0000-000000000405"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9945), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000613"), new Guid("00000000-0000-0000-0000-000000000405"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9959), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000614"), new Guid("00000000-0000-0000-0000-000000000405"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9967), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000615"), new Guid("00000000-0000-0000-0000-000000000406"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9974), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000616"), new Guid("00000000-0000-0000-0000-000000000406"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9982), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000617"), new Guid("00000000-0000-0000-0000-000000000406"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 497, DateTimeKind.Unspecified).AddTicks(9990), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000618"), new Guid("00000000-0000-0000-0000-000000000407"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(28), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000619"), new Guid("00000000-0000-0000-0000-000000000407"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(72), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000620"), new Guid("00000000-0000-0000-0000-000000000407"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(81), new TimeSpan(0, 3, 0, 0, 0)), "Created", null, new Guid("00000000-0000-0000-0000-000000000007") }
                });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "id", "content", "created_at", "is_anonymous", "product_id", "rating", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000501"), "'Great product, highly recommended!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1772), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000301"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000502"), "'Nice item, fast delivery.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1806), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000302"), 4, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000503"), "'Not as expected, disappointed.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1817), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000303"), 2, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000504"), "'Good quality, fair price.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1876), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000304"), 5, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000505"), "'Average product, nothing special.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1895), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000305"), 3, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000506"), "'Poor quality, avoid.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1905), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000306"), 1, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000507"), "'Excellent service, will buy again.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1915), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000307"), 5, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000508"), "'Decent product, worth the price.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1924), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000308"), 4, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000509"), "'Terrible experience, never again.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1932), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000309"), 1, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000510"), "'Impressive quality, exceeded expectations.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1972), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000310"), 5, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000511"), "'Fantastic product, changed my life!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1986), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000311"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000512"), "'Quick delivery, item as described.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1995), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000312"), 4, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000513"), "'Disappointed with the quality, not worth the price.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2004), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000313"), 2, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000514"), "'Great value for money, highly recommended.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2013), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000314"), 5, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000515"), "'Mediocre product, expected better.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2027), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000315"), 3, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000516"), "'Poor customer service, wouldn't recommend.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2037), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000316"), 1, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000517"), "'Absolutely thrilled with my purchase, thank you!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2068), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000317"), 5, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000518"), "'Satisfied with the product, would buy again.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2078), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000318"), 4, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000519"), "'Worst purchase ever, regret buying it.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2086), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000319"), 1, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000520"), "'Exceeded my expectations, excellent product!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2131), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000320"), 5, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000521"), "'Impressive quality, exceeded expectations!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2142), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000312"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000522"), "'Excellent performance, highly recommended!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2151), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000311"), 5, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000523"), "'Good value for money, would buy again.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2183), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000313"), 4, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000524"), "'Not as expected, quality was average.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2192), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000314"), 2, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000525"), "'Satisfied with the purchase, would recommend.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2206), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000315"), 4, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000526"), "'Poor quality, would not recommend.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2215), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000316"), 1, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000527"), "'Decent product, meets expectations.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2226), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000317"), 3, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000528"), "'Exceeded my expectations, will buy again.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2237), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000318"), 5, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000529"), "'Terrible service, not worth the price.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2269), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000319"), 1, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000530"), "'Quick delivery, product as described!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2285), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000320"), 5, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000531"), "'Comfortable and stylish, love it!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2297), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000302"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000532"), "'Very informative book, worth the price.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2312), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000303"), 4, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000533"), "'Durable and reliable, great for gardening.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2328), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000304"), 5, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000534"), "'Running shoes are lightweight, perfect!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2346), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000305"), 5, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000535"), "'Great for family entertainment, fun game!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2396), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000306"), 4, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000536"), "'Moisturizing and refreshing, my favorite shampoo!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2414), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000307"), 5, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000537"), "'Reliable battery, powers my car perfectly.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2430), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000308"), 4, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000538"), "'Very effective supplement, keeps me healthy!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2446), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000309"), 5, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000539"), "'Perfect breakfast cereal, balanced and nutritious.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2462), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000310"), 5, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000540"), "'Top-notch laptop, fantastic performance!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2518), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000311"), 5, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000541"), "'Very informative, comprehensive programming guide!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2531), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000303"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000542"), "'Durable and flexible, the best garden hose!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2584), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000304"), 5, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000543"), "'Lightweight and comfortable, fantastic running shoes!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2594), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000305"), 5, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000544"), "'Fun and engaging board game for families.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2605), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000306"), 4, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000545"), "'Great shampoo, keeps my hair smooth and shiny!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000307"), 5, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000546"), "'High-quality car battery, works perfectly!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2630), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000308"), 4, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000547"), "'Great supplement, keeps my vitamin levels up!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2640), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000309"), 5, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000548"), "'Nutritious and tasty cereal for my breakfast!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2665), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000310"), 4, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000549"), "'Best laptop for work and gaming, highly recommended!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2675), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000311"), 5, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000550"), "'Stylish dress, perfect for all occasions!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2689), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000312"), 5, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000551"), "'A wonderful e-book reader with a large display.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2700), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000313"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000552"), "'Very effective electric lawn mower for gardening.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2710), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000314"), 5, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000553"), "'A useful and accurate fitness tracker for workouts.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2720), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000315"), 4, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000554"), "'Fun and challenging puzzle game for mental exercise.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2762), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000316"), 3, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000555"), "'Great conditioner, keeps my hair soft and smooth!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2778), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000317"), 5, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000556"), "'Excellent car wax for a shiny finish!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2789), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000318"), 5, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000557"), "'Very effective protein powder for muscle growth!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2798), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000319"), 4, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000558"), "'Refreshing organic juice, tastes great!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000320"), 5, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000559"), "'This smartphone is incredible, great features!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2858), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000301"), 5, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000560"), "'Comfortable and versatile men’s t-shirt.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2901), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000302"), 4, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000561"), "'Durable and flexible, the best garden hose!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2911), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000304"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000562"), "'Lightweight and comfortable, fantastic running shoes!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2921), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000305"), 5, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000563"), "'Fun and engaging board game for families.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2931), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000306"), 4, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000564"), "'Great shampoo, keeps my hair smooth and shiny!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2945), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000307"), 5, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000565"), "'High-quality car battery, works perfectly!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2956), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000308"), 4, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000566"), "'Great supplement, keeps my vitamin levels up!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(2992), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000309"), 5, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000567"), "'Nutritious and tasty cereal for my breakfast!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3003), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000310"), 4, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000568"), "'Best laptop for work and gaming, highly recommended!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3013), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000311"), 5, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000569"), "'Stylish dress, perfect for all occasions!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3028), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000312"), 5, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000570"), "'A wonderful e-book reader with a large display.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3038), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000313"), 5, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000571"), "'Very effective electric lawn mower for gardening.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3048), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000314"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000572"), "'A useful and accurate fitness tracker for workouts.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3080), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000315"), 4, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000573"), "'Fun and challenging puzzle game for mental exercise.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3091), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000316"), 3, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000574"), "'Great conditioner, keeps my hair soft and smooth!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3106), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000317"), 5, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000575"), "'Excellent car wax for a shiny finish!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3116), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000318"), 5, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000576"), "'Very effective protein powder for muscle growth!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3126), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000319"), 4, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000577"), "'Refreshing organic juice, tastes great!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3136), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000320"), 5, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000578"), "'This smartphone is incredible, great features!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3168), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000301"), 5, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000579"), "'Comfortable and versatile men’s t-shirt.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3217), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000302"), 4, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000580"), "'Flexible garden hose, perfect for my garden!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3228), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000304"), 5, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000581"), "'Comfortable and well-fitting men’s t-shirt.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3237), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000302"), 4, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000582"), "'This smartphone is perfect for work and play.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3246), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000301"), 5, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000583"), "'Great book, very helpful for learning programming!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3257), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000303"), 5, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000584"), "'Flexible and long-lasting, a great garden hose.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3271), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000304"), 5, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000585"), "'Perfect fit and lightweight running shoes!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3381), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000305"), 4, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000586"), "'Fun and engaging board game for all ages.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3394), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000306"), 4, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000587"), "'Moisturizing shampoo, makes my hair shiny!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3405), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000307"), 5, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000588"), "'Reliable and long-lasting car battery.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3422), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000308"), 5, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000589"), "'Vitamin supplement, keeps my energy levels high!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3432), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000309"), 5, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000590"), "'Healthy and tasty cereal, ideal for breakfast.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3442), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000310"), 4, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000591"), "'Powerful and efficient laptop for multitasking!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3476), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000311"), 5, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000592"), "'Beautiful dress, elegant and comfortable!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3486), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000312"), 5, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000593"), "'E-book reader with great battery life and features.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3501), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000313"), 4, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000594"), "'Electric lawn mower that makes gardening easy.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3511), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000314"), 4, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000595"), "'Fitness tracker keeps me motivated to stay active!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3521), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000315"), 5, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000596"), "'Challenging puzzle game, keeps my mind sharp!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3531), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000316"), 4, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000597"), "'Smooth and nourishing conditioner, makes my hair soft!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3554), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000317"), 5, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000598"), "'Easy-to-apply car wax for a glossy finish.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3597), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000318"), 5, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000599"), "'High-quality protein powder for muscle growth.'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3608), new TimeSpan(0, 3, 0, 0, 0)), false, new Guid("00000000-0000-0000-0000-000000000319"), 5, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000600"), "'Organic juice with a refreshing flavor!'", new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(3618), new TimeSpan(0, 3, 0, 0, 0)), true, new Guid("00000000-0000-0000-0000-000000000320"), 4, null, new Guid("00000000-0000-0000-0000-000000000010") }
                });

            migrationBuilder.InsertData(
                table: "order_items",
                columns: new[] { "id", "created_at", "order_id", "order_id1", "price", "product_id", "quantity", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000801"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(682), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000601"), null, 99.99m, new Guid("00000000-0000-0000-0000-000000000301"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000802"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(733), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000602"), null, 49.99m, new Guid("00000000-0000-0000-0000-000000000302"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000803"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(743), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000603"), null, 149.99m, new Guid("00000000-0000-0000-0000-000000000303"), 3, null },
                    { new Guid("00000000-0000-0000-0000-000000000804"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(752), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000604"), null, 24.99m, new Guid("00000000-0000-0000-0000-000000000304"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000805"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(761), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000605"), null, 39.99m, new Guid("00000000-0000-0000-0000-000000000305"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000806"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(771), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000606"), null, 79.99m, new Guid("00000000-0000-0000-0000-000000000306"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000807"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(789), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000607"), null, 9.99m, new Guid("00000000-0000-0000-0000-000000000307"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000808"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(824), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000608"), null, 129.99m, new Guid("00000000-0000-0000-0000-000000000308"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000809"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(834), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000609"), null, 19.99m, new Guid("00000000-0000-0000-0000-000000000309"), 3, null },
                    { new Guid("00000000-0000-0000-0000-000000000810"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(843), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000610"), null, 3.99m, new Guid("00000000-0000-0000-0000-000000000310"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000811"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(852), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000611"), null, 999.99m, new Guid("00000000-0000-0000-0000-000000000311"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000812"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(860), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000612"), null, 59.99m, new Guid("00000000-0000-0000-0000-000000000312"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000813"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(875), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000613"), null, 149.99m, new Guid("00000000-0000-0000-0000-000000000313"), 3, null },
                    { new Guid("00000000-0000-0000-0000-000000000814"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(884), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000614"), null, 199.99m, new Guid("00000000-0000-0000-0000-000000000314"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000815"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(893), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000615"), null, 79.99m, new Guid("00000000-0000-0000-0000-000000000315"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000816"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(924), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000616"), null, 14.99m, new Guid("00000000-0000-0000-0000-000000000316"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000817"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(933), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000617"), null, 12.99m, new Guid("00000000-0000-0000-0000-000000000317"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000818"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(943), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000618"), null, 39.99m, new Guid("00000000-0000-0000-0000-000000000318"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000819"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(957), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000619"), null, 29.99m, new Guid("00000000-0000-0000-0000-000000000319"), 3, null },
                    { new Guid("00000000-0000-0000-0000-000000000820"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(966), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000620"), null, 4.99m, new Guid("00000000-0000-0000-0000-000000000320"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000821"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(974), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000611"), null, 99.99m, new Guid("00000000-0000-0000-0000-000000000301"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000822"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(983), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000612"), null, 49.99m, new Guid("00000000-0000-0000-0000-000000000302"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000823"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1013), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000613"), null, 149.99m, new Guid("00000000-0000-0000-0000-000000000303"), 3, null },
                    { new Guid("00000000-0000-0000-0000-000000000824"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1023), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000614"), null, 24.99m, new Guid("00000000-0000-0000-0000-000000000304"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000825"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1032), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000615"), null, 39.99m, new Guid("00000000-0000-0000-0000-000000000305"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000826"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1082), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000616"), null, 79.99m, new Guid("00000000-0000-0000-0000-000000000306"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000827"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1093), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000617"), null, 9.99m, new Guid("00000000-0000-0000-0000-000000000307"), 2, null },
                    { new Guid("00000000-0000-0000-0000-000000000828"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1102), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000618"), null, 129.99m, new Guid("00000000-0000-0000-0000-000000000308"), 1, null },
                    { new Guid("00000000-0000-0000-0000-000000000829"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1110), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000619"), null, 19.99m, new Guid("00000000-0000-0000-0000-000000000309"), 3, null },
                    { new Guid("00000000-0000-0000-0000-000000000830"), new DateTimeOffset(new DateTime(2024, 5, 7, 18, 11, 47, 498, DateTimeKind.Unspecified).AddTicks(1137), new TimeSpan(0, 3, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000620"), null, 3.99m, new Guid("00000000-0000-0000-0000-000000000310"), 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_user_id",
                table: "addresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_images_product_id",
                table: "images",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_images_review_id",
                table: "images",
                column: "review_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id_product_id",
                table: "order_items",
                columns: new[] { "order_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id1",
                table: "order_items",
                column: "order_id1");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_address_id",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_id",
                table: "reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
