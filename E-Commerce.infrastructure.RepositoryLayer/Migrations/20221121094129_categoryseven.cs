﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.infrastructure.RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class categoryseven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Category");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Category",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Category",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Category");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
